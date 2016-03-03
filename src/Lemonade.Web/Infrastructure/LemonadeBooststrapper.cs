using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Lemonade.Web.EventHandlers;
using Lemonade.Web.Events;
using Microsoft.AspNet.SignalR;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Hosting.Aspnet;
using Nancy.Responses;
using Nancy.TinyIoc;
using Nancy.ViewEngines;

namespace Lemonade.Web.Infrastructure
{
    public class LemonadeBootstrapper : DefaultNancyBootstrapper, IDomainEventDispatcher
    {
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

            foreach (var assembly in AppDomainAssemblyTypeScanner.Assemblies)
            {
                MapResourcesFromAssembly(conventions, assembly);
            }
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            _container = container;
            _container.Register<IDomainEventHandler<ConfigurationHasBeenCreated>, ConfigurationHasBeenCreatedHandler>();
            _container.Register<IDomainEventHandler<ConfigurationHasBeenUpdated>, ConfigurationHasBeenUpdatedHandler>();
            _container.Register<IDomainEventHandler<ConfigurationHasBeenDeleted>, ConfigurationHasBeenDeletedHandler>();
            _container.Register<IDomainEventHandler<ConfigurationErrorHasOccurred>, ConfigurationErrorHasOccurredHandler>();

            _container.Register<IDomainEventHandler<ApplicationHasBeenCreated>, ApplicationHasBeenCreatedHandler>();
            _container.Register<IDomainEventHandler<ApplicationHasBeenUpdated>, ApplicationHasBeenUpdatedHandler>();
            _container.Register<IDomainEventHandler<ApplicationHasBeenDeleted>, ApplicationHasBeenDeletedHandler>();
            _container.Register<IDomainEventHandler<ApplicationErrorHasOccurred>, ApplicationErrorHasOccurredHandler>();

            _container.Register<IDomainEventHandler<FeatureHasBeenCreated>, FeatureHasBeenCreatedHandler>();
            _container.Register<IDomainEventHandler<FeatureHasBeenUpdated>, FeatureHasBeenUpdatedHandler>();
            _container.Register<IDomainEventHandler<FeatureHasBeenDeleted>, FeatureHasBeenDeletedHandler>();
            _container.Register<IDomainEventHandler<FeatureErrorHasOccurred>, FeatureErrorHasOccurredHandler>();

            _container.Register<IDomainEventHandler<FeatureOverrideHasBeenCreated>, FeatureOverrideHasBeenCreatedHandler>();
            _container.Register<IDomainEventHandler<FeatureOverrideHasBeenUpdated>, FeatureOverrideHasBeenUpdatedHandler>();
            _container.Register<IDomainEventHandler<FeatureOverrideHasBeenDeleted>, FeatureOverrideHasBeenDeletedHandler>();
            _container.Register<IDomainEventHandler<FeatureOverrideErrorHasOccurred>, FeatureOverrideErrorHasOccurredHandler>();

            _container.Register<IDomainEventHandler<ResourceHasBeenCreated>, ResourceHasBeenCreatedHandler>();
            _container.Register<IDomainEventHandler<ResourceHasBeenUpdated>, ResourceHasBeenUpdatedHandler>();
            _container.Register<IDomainEventHandler<ResourceHasBeenDeleted>, ResourceHasBeenDeletedHandler>();
            _container.Register<IDomainEventHandler<ResourceErrorHasOccurred>, ResourceErrorHasOccurredHandler>();

            _container.Register<IDomainEventDispatcher>(this);

            _container.Register(GlobalHost.ConnectionManager);

            ConfigureDependencies(_container);

            _dependencies.ForEach(d => d(_container));
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get { return NancyInternalConfiguration.WithOverrides(nic => nic.ViewLocationProvider = typeof(ResourceViewLocationProvider)); }
        }

        private static void MapResourcesFromAssembly(NancyConventions conventions, Assembly assembly)
        {
            var resourceNames = assembly.GetManifestResourceNames();

            conventions.StaticContentsConventions.Add((ctx, p) =>
            {
                var directoryName = Path.GetDirectoryName(ctx.Request.Path);
                var path = assembly.GetName().Name + directoryName?.Replace(Path.DirectorySeparatorChar, '.').Replace("-", "_");
                var file = Path.GetFileName(ctx.Request.Path);
                var name = string.Concat(path, ".", file);

                return resourceNames.Any(r => r.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    ? new EmbeddedFileResponse(assembly, path, file)
                    : null;
            });
        }

        protected override IRootPathProvider RootPathProvider => new AspNetRootPathProvider();

        private readonly List<Action<TinyIoCContainer>> _dependencies = new List<Action<TinyIoCContainer>>();
        private TinyIoCContainer _container;
    }
}