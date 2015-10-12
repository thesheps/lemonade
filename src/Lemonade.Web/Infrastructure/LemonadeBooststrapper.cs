using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lemonade.Web.EventHandlers;
using Lemonade.Web.Events;
using Lemonade.Web.Modules;
using Microsoft.AspNet.SignalR;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Hosting.Aspnet;
using Nancy.Responses;
using Nancy.TinyIoc;
using Nancy.ViewEngines;
using Nancy.ViewEngines.Razor;

namespace Lemonade.Web.Infrastructure
{
    public class LemonadeBootstrapper : DefaultNancyBootstrapper, IDomainEventDispatcher
    {
        public LemonadeBootstrapper()
        {
            DomainEvent.Dispatcher = this;
        }

        public void Dispatch<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            _container.ResolveAll<IDomainEventHandler<TEvent>>().ToList().ForEach(h => h.Handle(@event));
        }

        public void AddDependency(Action<TinyIoCContainer> dependency)
        {
            _dependencies.Add(dependency);
        }

        protected virtual void ConfigureDependencies(TinyIoCContainer container)
        {
        }

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            var assembly = typeof(LemonadeBootstrapper).Assembly;
            var resourceNames = assembly.GetManifestResourceNames();

            conventions.StaticContentsConventions.Add((ctx, p) =>
            {
                var directoryName = Path.GetDirectoryName(ctx.Request.Path);
                var path = assembly.GetName().Name + directoryName?.Replace(Path.DirectorySeparatorChar, '.').Replace("-", "_");
                var file = Path.GetFileName(ctx.Request.Path);
                var name = string.Concat(path, ".", file);

                return resourceNames.Any(r => r.Equals(name, StringComparison.InvariantCultureIgnoreCase)) ? new EmbeddedFileResponse(assembly, path, file) : null;
            });
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            _container = container;
            _container.Register<IDomainEventHandler<ApplicationHasBeenCreated>, ApplicationHasBeenCreatedHandler>();
            _container.Register<IDomainEventHandler<ApplicationHasBeenDeleted>, ApplicationHasBeenDeletedHandler>();
            _container.Register<IDomainEventHandler<FeatureHasBeenCreated>, FeatureHasBeenCreatedHandler>();
            _container.Register<IDomainEventHandler<FeatureHasBeenDeleted>, FeatureHasBeenDeletedHandler>();
            _container.Register<IDomainEventHandler<ErrorHasOccurred>, ErrorHasOccurredHandler>();
            _container.Register(GlobalHost.ConnectionManager);

            ConfigureDependencies(_container);

            _dependencies.ForEach(d => d(_container));

            ResourceViewLocationProvider.RootNamespaces.Clear();
            ResourceViewLocationProvider.RootNamespaces.Add(typeof(FeaturesModule).Assembly, "Lemonade.Web.Views");
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get { return NancyInternalConfiguration.WithOverrides(nic => nic.ViewLocationProvider = typeof(ResourceViewLocationProvider)); }
        }

        protected override IEnumerable<Type> ViewEngines { get { yield return typeof(RazorViewEngine); } }
        protected override IRootPathProvider RootPathProvider => new AspNetRootPathProvider();
        private readonly List<Action<TinyIoCContainer>> _dependencies = new List<Action<TinyIoCContainer>>();
        private TinyIoCContainer _container;
    }
}