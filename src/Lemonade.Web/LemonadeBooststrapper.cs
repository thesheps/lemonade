using System;
using System.Collections.Generic;
using Lemonade.Web.Modules;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Nancy.ViewEngines;
using Nancy.ViewEngines.Razor;

namespace Lemonade.Web
{
    public abstract class LemonadeBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            ResourceViewLocationProvider.RootNamespaces.Clear();
            ResourceViewLocationProvider.RootNamespaces.Add(typeof(FeatureModule).Assembly, "Lemonade.Web.Views");
            ConfigureDependencies(container);
        }

        protected abstract void ConfigureDependencies(TinyIoCContainer container);

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get { return NancyInternalConfiguration.WithOverrides(nic => nic.ViewLocationProvider = typeof(ResourceViewLocationProvider)); }
        }

        protected override IEnumerable<Type> ViewEngines { get { yield return typeof(RazorViewEngine); } }
    }
}