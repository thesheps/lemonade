using System.Collections.Generic;
using Lemonade.Sql.Migrations;
using Lemonade.Web.Contracts;
using Lemonade.Web.Tests.Mocks;
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

            _bootstrapper = new TestBootstrapper();
            _browser = new Browser(_bootstrapper, context => context.UserHostAddress("TEST"));
            Post(new Application { Name = "TestApplication1", ApplicationId = 1 });
        }

        [TearDown]
        public void Teardown()
        {
            _server.Stop();
            _server.Dispose();
        }

        [Test]
        public void WhenIPostAConfiguration_ThenICanGetItViaHttpAndSignalRClientsAreNotified()
        {
            var configuration = new Contracts.Configuration { Name = "TestConfig", Value = "Hello World", ConfigurationId = 1, ApplicationId = 1 };
            Post(configuration);

            var response = _browser.Get("/api/configurations", with =>
            {
                with.Header("Accept", "application/json");
                with.Query("applicationId", configuration.ApplicationId.ToString());
            });

            var result = JsonConvert.DeserializeObject<List<Contracts.Configuration>>(response.Body.AsString());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("TestConfig"));

            _bootstrapper
                .Resolve<IMockClient>()
                .Received()
                .addConfiguration(Arg.Any<dynamic>());
        }

        [Test]
        public void WhenIPutAConfiguration_ThenTheConfigurationIsUpdatedAndSignalRClientsAreNotified()
        {
            var configuration = new Contracts.Configuration { Name = "TestConfig", Value = "Test", ConfigurationId = 1, ApplicationId = 1 };
            Post(configuration);
            Put(new Contracts.Configuration { Name = "PONIES", Value = "Updated", ConfigurationId = 1 });

            var response = _browser.Get("/api/configurations", with =>
            {
                with.Header("Accept", "application/json");
                with.Query("applicationId", configuration.ApplicationId.ToString());
            });

            var result = JsonConvert.DeserializeObject<List<Contracts.Configuration>>(response.Body.AsString());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("PONIES"));

            _bootstrapper
                .Resolve<IMockClient>()
                .Received()
                .updateConfiguration(Arg.Any<dynamic>());
        }

        [Test]
        public void WhenIDeleteAConfiguration_ThenItIsNoLongerAvailableAndSignalRClientsAreNotified()
        {
            Post(new Contracts.Configuration { Name = "TestConfig", Value = "Test", ConfigurationId = 1, ApplicationId = 1 });
            Delete(new Contracts.Configuration { ConfigurationId = 1 });

            var response = _browser.Get("/api/configurations", with => with.Header("Accept", "application/json"));
            var result = JsonConvert.DeserializeObject<List<Contracts.Configuration>>(response.Body.AsString());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Count, Is.EqualTo(0));

            _bootstrapper
                .Resolve<IMockClient>()
                .Received()
                .removeConfiguration(Arg.Any<dynamic>());
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

        private Server _server;
        private Browser _browser;
        private TestBootstrapper _bootstrapper;
        private const string ConnectionString = "Lemonade";
    }
}