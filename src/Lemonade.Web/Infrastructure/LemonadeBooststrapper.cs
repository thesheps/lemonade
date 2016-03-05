using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
    public class LemonadeBootstrapper : DefaultNancyBootstrapper
    {
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
            container
                .InstallCommandHandlers()
                .InstallDomainEventHandlers();

            base.ConfigureApplicationContainer(container);

            container.Register(GlobalHost.ConnectionManager);

            ConfigureDependencies(container);

            _dependencies.ForEach(d => d(container));
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
    }
}