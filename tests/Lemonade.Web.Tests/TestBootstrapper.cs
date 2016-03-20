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
        protected override IConnectionManager GetConnectionManager()
        {
            var hubContext = Substitute.For<IHubContext>();
            var connectionManager = Substitute.For<IConnectionManager>();
            connectionManager.GetHubContext<LemonadeHub>().Returns(hubContext);

            return connectionManager;
        }

        protected override void ConfigureDependencies(TinyIoCContainer container)
        {
            _container = container;
            _container.Register<ICreateApplication, CreateApplicationFake>();
            _container.Register<ICreateConfiguration, CreateConfigurationFake>();
            _container.Register<ICreateFeature, CreateFeatureFake>();
            _container.Register<ICreateFeatureOverride, CreateFeatureOverrideFake>();
            _container.Register<ICreateResource, CreateResourceFake>();

            var mockClient = Substitute.For<IMockClient>();
            var connectionManager = container.Resolve<IConnectionManager>();
            var hubContext = connectionManager.GetHubContext<LemonadeHub>();
            SubstituteExtensions.Returns(hubContext.Clients.All, mockClient);

            _container.Register(mockClient);
        }

        public T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        private TinyIoCContainer _container;
    }
}