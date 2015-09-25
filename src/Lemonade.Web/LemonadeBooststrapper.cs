using System;
using System.Collections.Generic;
using Lemonade.Core.Events;
using Lemonade.Web.EventHandlers;
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
        protected LemonadeBootstrapper()
        {
            DomainEvent.Dispatcher = this;
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            _container = container;

            base.ConfigureApplicationContainer(container);

            ConfigureEventHandlers();
            ConfigureDependencies(container);

            ResourceViewLocationProvider.RootNamespaces.Clear();
            ResourceViewLocationProvider.RootNamespaces.Add(typeof(FeatureModule).Assembly, "Lemonade.Web.Views");
        }

        public void Dispatch<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            var handler = _container.Resolve<IDomainEventHandler<TEvent>>();
            handler.Handle(@event);
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get { return NancyInternalConfiguration.WithOverrides(nic => nic.ViewLocationProvider = typeof(ResourceViewLocationProvider)); }
        }

        private void ConfigureEventHandlers()
        {
            _container.Register<IDomainEventHandler<ApplicationHasBeenSaved>, ApplicationHasBeenSavedHandler>();
        }

        protected override IEnumerable<Type> ViewEngines { get { yield return typeof(RazorViewEngine); } }
        protected override IRootPathProvider RootPathProvider => new AspNetRootPathProvider();
        protected abstract void ConfigureDependencies(TinyIoCContainer container);

        private TinyIoCContainer _container;
    }
}