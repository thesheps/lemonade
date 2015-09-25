using System;
using System.Collections.Generic;
using Lemonade.Core.Events;
using Lemonade.Web.Modules;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Hosting.Aspnet;
using Nancy.TinyIoc;
using Nancy.ViewEngines;
using Nancy.ViewEngines.Razor;

namespace Lemonade.Web
{
    public abstract class LemonadeBootstrapper : DefaultNancyBootstrapper, IDomainEventDispatcher
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            _container = container;

            base.ConfigureApplicationContainer(container);
            ResourceViewLocationProvider.RootNamespaces.Clear();
            ResourceViewLocationProvider.RootNamespaces.Add(typeof(FeatureModule).Assembly, "Lemonade.Web.Views");
            ConfigureDependencies(container);
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get { return NancyInternalConfiguration.WithOverrides(nic => nic.ViewLocationProvider = typeof(ResourceViewLocationProvider)); }
        }

        public void Dispatch<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<Type> ViewEngines { get { yield return typeof(RazorViewEngine); } }
        protected override IRootPathProvider RootPathProvider => new AspNetRootPathProvider();
        protected abstract void ConfigureDependencies(TinyIoCContainer container);

        private TinyIoCContainer _container;
    }
}