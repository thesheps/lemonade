using System.Collections.Generic;
using Lemonade.Sql.Migrations;
using Lemonade.Web.Contracts;
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
    public class GivenApplicationModule
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

            var bootstrapper = new TestLemonadeBootstrapper();
            bootstrapper.ConfigureDependency(c => c.Register(connectionManager));

            _browser = new Browser(bootstrapper);
        }

        [TearDown]
        public void Teardown()
        {
            _server.Dispose();
        }

        [Test]
        public void WhenIPostAnApplication_ThenICanGetItViaHttp()
        {
            _browser.Post("/api/application", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(new Application { Name = "TestApplication1" }));
            });

            var response = _browser.Get("/api/application", with => with.Header("Accept", "application/json"));
            var result = JsonConvert.DeserializeObject<List<Application>>(response.Body.AsString());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("TestApplication1"));
        }

        [Test]
        public void WhenIPostAnApplication_ThenSignalRClientsAreNotified()
        {
            _browser.Post("/api/application/", (with) =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(new Application { Name = "TestApplication1" }));
            });

            _mockClient.Received().addApplication(Arg.Any<dynamic>());
        }

        [Test]
        public void WhenIPostAnApplicationAndDeleteIt_ThenItIsNoLongerAvailable()
        {
            _browser.Post("/api/application", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(new Application { Name = "TestApplication1" }));
            });

            _browser.Delete("/api/application", with =>
            {
                with.Query("id", "1");
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(new Application { Name = "TestApplication1" }));
            });

            var response = _browser.Get("/api/application", with => with.Header("Accept", "application/json"));
            var result = JsonConvert.DeserializeObject<List<Application>>(response.Body.AsString());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Count, Is.EqualTo(0));
        }

        private Server _server;
        private Browser _browser;
        private IMockClient _mockClient;
        private const string ConnectionString = "Lemonade";
    }
}