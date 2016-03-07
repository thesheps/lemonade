using Lemonade.Data.Commands;
using Lemonade.Fakes;
using Lemonade.Web.Infrastructure;
using Lemonade.Web.Tests.Mocks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Nancy.TinyIoc;
using NSubstitute;

namespace Lemonade.Web.Tests
{
    public class TestBootstrapper : LemonadeBootstrapper
    {
        protected override void ConfigureDependencies(TinyIoCContainer container)
        {
            _container = container;

            var hubContext = Substitute.For<IHubContext>();
            var connectionManager = Substitute.For<IConnectionManager>();
            connectionManager.GetHubContext<LemonadeHub>().Returns(hubContext);

            var mockClient = Substitute.For<IMockClient>();
            SubstituteExtensions.Returns(hubContext.Clients.All, mockClient);

            container.Register(mockClient);
            container.Register(connectionManager);
            container.Register<ICreateApplication, CreateApplicationFake>();
            container.Register<ICreateConfiguration, CreateConfigurationFake>();
            container.Register<ICreateFeature, CreateFeatureFake>();
            container.Register<ICreateFeatureOverride, CreateFeatureOverrideFake>();
            container.Register<ICreateResource, CreateResourceFake>();
        }

        public T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        private TinyIoCContainer _container;
    }
}