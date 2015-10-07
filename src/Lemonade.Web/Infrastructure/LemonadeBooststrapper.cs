using System;
using System.Collections.Generic;
using Lemonade.Core.Events;
using Lemonade.Web.EventHandlers;
using Lemonade.Web.Modules;
using Microsoft.AspNet.SignalR;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Hosting.Aspnet;
using Nancy.TinyIoc;
using Nancy.ViewEngines;
using Nancy.ViewEngines.Razor;

namespace Lemonade.Web.Infrastructure
{
    public abstract class LemonadeBootstrapper : DefaultNancyBootstrapper, IDomainEventDispatcher
    {
        protected LemonadeBootstrapper()
        {
            DomainEvent.Dispatcher = this;
        }

        public void Dispatch<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            var handler = _container.Resolve<IDomainEventHandler<TEvent>>();
            handler.Handle(@event);
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

            ResourceViewLocationProvider.RootNamespaces.Clear();
            ResourceViewLocationProvider.RootNamespaces.Add(typeof(FeaturesModule).Assembly, "Lemonade.Web.Views");
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get { return NancyInternalConfiguration.WithOverrides(nic => nic.ViewLocationProvider = typeof(ResourceViewLocationProvider)); }
        }

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("scripts", "bin/content/scripts"));
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("styles", "bin/content/styles"));
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("fonts", "bin/content/fonts"));

            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("scripts", "bin/scripts"));
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("styles", "bin/styles"));
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("fonts", "bin/fonts"));
        }

        protected override IEnumerable<Type> ViewEngines { get { yield return typeof(RazorViewEngine); } }
        protected override IRootPathProvider RootPathProvider => new AspNetRootPathProvider();
        protected abstract void ConfigureDependencies(TinyIoCContainer container);

        private TinyIoCContainer _container;
    }
}