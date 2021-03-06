﻿using System.Collections.Generic;
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
    public class GivenApplicationsModule
    {
        [SetUp]
        public void SetUp()
        {
            _server = new Server(64978);
            Runner.SqlCompact(ConnectionString).Down();
            Runner.SqlCompact(ConnectionString).Up();

            _bootstrapper = new TestBootstrapper();
            _browser = new Browser(_bootstrapper, context => context.UserHostAddress("TEST"));
        }

        [TearDown]
        public void Teardown()
        {
            _server.Stop();
            _server.Dispose();
        }

        [Test]
        public void WhenIPostAnApplication_ThenICanGetItViaHttpAndSignalRClientsAreNotified()
        {
            Post(new Application { Name = "TestApplication1" });

            var response = _browser.Get("/api/applications", with => with.Header("Accept", "application/json"));
            var result = JsonConvert.DeserializeObject<List<Application>>(response.Body.AsString());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("TestApplication1"));

            _bootstrapper
                .Resolve<IMockClient>()
                .Received()
                .addApplication(Arg.Any<dynamic>());
        }

        [Test]
        public void WhenIPutAnApplication_ThenTheApplicationIsUpdatedAndSignalRClientsAreUpdated()
        {
            Post(new Application { Name = "TestApplication1" });
            Put(new Application { Name = "PONIES", ApplicationId = 1 });

            var response = _browser.Get("/api/applications", with => with.Header("Accept", "application/json"));
            var result = JsonConvert.DeserializeObject<List<Application>>(response.Body.AsString());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("PONIES"));

            _bootstrapper
                .Resolve<IMockClient>()
                .Received()
                .updateApplication(Arg.Any<dynamic>());
        }

        [Test]
        public void WhenIDeleteAnApplication_ThenItIsNoLongerAvailableAndSignalRClientsAreNotified()
        {
            Post(new Application { Name = "TestApplication1" });
            Delete(new Application { Name = "TestApplication1" });

            var response = _browser.Get("/api/applications", with => with.Header("Accept", "application/json"));
            var result = JsonConvert.DeserializeObject<List<Application>>(response.Body.AsString());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Count, Is.EqualTo(0));

            _bootstrapper
                .Resolve<IMockClient>()
                .Received()
                .removeApplication(Arg.Any<dynamic>());
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

        private Server _server;
        private Browser _browser;
        private TestBootstrapper _bootstrapper;
        private const string ConnectionString = "Lemonade";
    }
}