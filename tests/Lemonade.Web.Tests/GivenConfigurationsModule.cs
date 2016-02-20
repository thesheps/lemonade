using System.Collections.Generic;
using Lemonade.Sql.Migrations;
using Lemonade.Web.Contracts;
using Lemonade.Web.Infrastructure;
using Lemonade.Web.Tests.Mocks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using SelfishHttp;

namespace Lemonade.Web.Tests
{
    public class GivenConfigurationsModule
    {
        [SetUp]
        public void SetUp()
        {
            _server = new Server(64978);
            Runner.SqlCompact(ConnectionString).Down();
            Runner.SqlCompact(ConnectionString).Up();

            var hubContext = Substitute.For<IHubContext>();
            var connectionManager = Substitute.For<IConnectionManager>();
            connectionManager.GetHubContext<LemonadeHub>().Returns(hubContext);

            _mockClient = Substitute.For<IMockClient>();
            SubstituteExtensions.Returns(hubContext.Clients.All, _mockClient);

            var bootstrapper = new LemonadeBootstrapper();
            bootstrapper.AddDependency(c => c.Register(connectionManager));

            _browser = new Browser(bootstrapper, context => context.UserHostAddress("TEST"));
            Post(new Application { Name = "TestApplication1", ApplicationId = 1 });
        }

        [TearDown]
        public void Teardown()
        {
            _server.Stop();
            _server.Dispose();
        }

        [Test]
        public void WhenIPostAConfiguration_ThenICanGetItViaHttp()
        {
            Post(new Contracts.Configuration { Name = "TestConfig", Value = "Hello World", ConfigurationId = 1, ApplicationId = 1 });

            var response = _browser.Get("/api/configurations", with => with.Header("Accept", "application/json"));
            var result = JsonConvert.DeserializeObject<List<Contracts.Configuration>>(response.Body.AsString());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("TestConfig"));
        }

        [Test]
        public void WhenIPostAConfiguration_ThenSignalRClientsAreNotified()
        {
            Post(new Contracts.Configuration { Name = "TestConfig", Value = "Test", ConfigurationId = 1, ApplicationId = 1 });
            _mockClient.Received().addConfiguration(Arg.Any<dynamic>());
        }

        [Test]
        public void WhenIPutAConfiguration_ThenTheConfigurationIsUpdated()
        {
            Post(new Contracts.Configuration { Name = "TestConfig", Value = "Test", ConfigurationId = 1, ApplicationId = 1 });
            Put(new Contracts.Configuration { Name = "PONIES", Value = "Updated", ConfigurationId = 1 });

            var response = _browser.Get("/api/configurations", with => with.Header("Accept", "application/json"));
            var result = JsonConvert.DeserializeObject<List<Contracts.Configuration>>(response.Body.AsString());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("PONIES"));
        }

        [Test]
        public void WhenIDeleteAConfiguration_ThenItIsNoLongerAvailable()
        {
            Post(new Contracts.Configuration { Name = "TestConfig", Value = "Test", ConfigurationId = 1, ApplicationId = 1 });
            Delete(new Contracts.Configuration { ConfigurationId = 1 });

            var response = _browser.Get("/api/configurations", with => with.Header("Accept", "application/json"));
            var result = JsonConvert.DeserializeObject<List<Contracts.Configuration>>(response.Body.AsString());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void WhenIDeleteAConfiguration_ThenSignalRClientsAreNotified()
        {
            Post(new Contracts.Configuration { Name = "TestConfig", Value = "Test", ConfigurationId = 1, ApplicationId = 1 });
            Delete(new Contracts.Configuration { ConfigurationId = 1 });
            _mockClient.Received().removeConfiguration(Arg.Any<dynamic>());
        }

        private void Post(Application application)
        {
            _browser.Post("/api/applications", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(application));
            });
        }

        private void Post(Contracts.Configuration configuration)
        {
            _browser.Post("/api/configurations", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(configuration));
            });
        }

        private void Put(Contracts.Configuration configuration)
        {
            _browser.Put("/api/configurations", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(configuration));
            });
        }

        private void Delete(Contracts.Configuration configuration)
        {
            _browser.Delete("/api/configurations", with =>
            {
                with.Query("id", "1");
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(configuration));
            });
        }

        private IMockClient _mockClient;
        private Server _server;
        private Browser _browser;
        private const string ConnectionString = "Lemonade";
    }
}