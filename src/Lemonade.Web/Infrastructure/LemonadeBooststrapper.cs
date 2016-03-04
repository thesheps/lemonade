using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Events;
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
    public class LemonadeBootstrapper : DefaultNancyBootstrapper, IDomainEventDispatcher, ICommandDispatcher
    {
        void IDomainEventDispatcher.Dispatch<TEvent>(TEvent @event)
        {
            _container.ResolveAll<IDomainEventHandler<TEvent>>().ToList().ForEach(h => h.Handle(@event));
        }

        void ICommandDispatcher.Dispatch<TCommand>(TCommand command)
        {
            _container.ResolveAll<ICommandHandler<TCommand>>().ToList().ForEach(h => h.Handle(command));
        }

        public void AddDependency(Action<TinyIoCContainer> dependency)
        {
            _dependencies.Add(dependency);
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

            DomainEventInstaller.Install(container);
            CommandInstaller.Install(container);

            container.Register<IDomainEventDispatcher>(this);
            container.Register<ICommandDispatcher>(this);
            container.Register(GlobalHost.ConnectionManager);

            ConfigureDependencies(container);

            _dependencies.ForEach(d => d(container));

            _container = container;
        }

        protected virtual void ConfigureDependencies(TinyIoCContainer container)
        {
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get { return NancyInternalConfiguration.WithOverrides(nic => nic.ViewLocationProvider = typeof(ResourceViewLocationProvider)); }
        }

        protected override IRootPathProvider RootPathProvider => new AspNetRootPathProvider();

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

        private readonly List<Action<TinyIoCContainer>> _dependencies = new List<Action<TinyIoCContainer>>();
        private TinyIoCContainer _container;
    }
}