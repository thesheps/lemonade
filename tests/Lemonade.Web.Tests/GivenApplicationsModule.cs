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
    public class GivenApplicationsModule
    {
        [SetUp]
        public void SetUp()
        {
            _server = new Server(64978);
            Runner.SqlCompact(ConnectionString).Down();
            Runner.SqlCompact(ConnectionString).Up();

            var hubContext = Substitute.For<IHubContext>();
            var connectionManager = Substitute.For<IConnectionManager>();
            connectionManager.GetHubContext<FeatureHub>().Returns(hubContext);

            _mockClient = Substitute.For<IMockClient>();
            SubstituteExtensions.Returns(hubContext.Clients.All, _mockClient);

            var bootstrapper = new LemonadeBootstrapper();
            bootstrapper.AddDependency(c => c.Register(connectionManager));

            _browser = new Browser(bootstrapper, context => context.UserHostAddress("TEST"));
        }

        [TearDown]
        public void Teardown()
        {
            _server.Stop();
            _server.Dispose();
        }

        [Test]
        public void WhenIPostAnApplication_ThenICanGetItViaHttp()
        {
            Post(new Application { Name = "TestApplication1" });

            var response = _browser.Get("/api/applications", with => with.Header("Accept", "application/json"));
            var result = JsonConvert.DeserializeObject<List<Application>>(response.Body.AsString());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("TestApplication1"));
        }

        [Test]
        public void WhenIPostAnApplication_ThenSignalRClientsAreNotified()
        {
            Post(new Application { Name = "TestApplication1" });
            _mockClient.Received().addApplication(Arg.Any<dynamic>());
        }

        [Test]
        public void WhenIPutAnApplication_ThenTheApplicationIsUpdated()
        {
            Post(new Application { Name = "TestApplication1" });
            Put(new Application { Name = "PONIES", ApplicationId = 1 });

            var response = _browser.Get("/api/applications", with => with.Header("Accept", "application/json"));
            var result = JsonConvert.DeserializeObject<List<Application>>(response.Body.AsString());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("PONIES"));
        }

        [Test]
        public void WhenIDeleteAnApplication_ThenItIsNoLongerAvailable()
        {
            Post(new Application { Name = "TestApplication1" });
            Delete(new Application { Name = "TestApplication1" });

            var response = _browser.Get("/api/applications", with => with.Header("Accept", "application/json"));
            var result = JsonConvert.DeserializeObject<List<Application>>(response.Body.AsString());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void WhenIDeleteAnApplication_ThenSignalRClientsAreNotified()
        {
            Post(new Application { Name = "TestApplication1" });
            Delete(new Application { Name = "TestApplication1" });
            _mockClient.Received().removeApplication(Arg.Any<dynamic>());
        }

        private void Post(Application application)
        {
            _browser.Post("/api/applications", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(application));
            });
        }

        private void Put(Application application)
        {
            _browser.Put("/api/applications", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(application));
            });
        }

        private void Delete(Application application)
        {
            _browser.Delete("/api/applications", with =>
            {
                with.Query("id", "1");
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(application));
            });
        }

        private IMockClient _mockClient;
        private Server _server;
        private Browser _browser;
        private const string ConnectionString = "Lemonade";
    }
}