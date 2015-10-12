using System;
using System.Collections.Generic;
using Lemonade.Web.EventHandlers;
using Lemonade.Web.Events;
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
    public class LemonadeBootstrapper : DefaultNancyBootstrapper, IDomainEventDispatcher
    {
        public LemonadeBootstrapper()
        {
            DomainEvent.Dispatcher = this;
        }

        public void Dispatch<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            var handlers = _container.ResolveAll<IDomainEventHandler<TEvent>>();

            foreach (var domainEventHandler in handlers)
            {
                domainEventHandler.Handle(@event);
            }
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

            foreach (var dependency in _dependencies)
                dependency(_container);

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
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("scripts", "bin/content"));
        }

        protected virtual void ConfigureDependencies(TinyIoCContainer container)
        {
        }

        public void AddDependency(Action<TinyIoCContainer> dependency)
        {
            _dependencies.Add(dependency);
        }

        protected override IEnumerable<Type> ViewEngines { get { yield return typeof(RazorViewEngine); } }
        protected override IRootPathProvider RootPathProvider => new AspNetRootPathProvider();
        private TinyIoCContainer _container;
        private readonly IList<Action<TinyIoCContainer>> _dependencies = new List<Action<TinyIoCContainer>>();
    }
}