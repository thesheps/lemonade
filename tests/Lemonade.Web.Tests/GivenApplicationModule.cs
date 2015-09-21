using System.Collections.Generic;
using System.Linq;
using Lemonade.Sql.Migrations;
using Lemonade.Web.Contracts;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
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
            Runner.Sqlite(ConnectionString).Down();
            Runner.Sqlite(ConnectionString).Up();

            _browser = new Browser(new FakeLemonadeBootstrapper());
        }

        [TearDown]
        public void Teardown()
        {
            _server.Dispose();
        }

        [Test]
        public void WhenIPostANewApplication_ThenTheResponseIsRedirectToFeaturesPage()
        {
            var postResponse = _browser.Post("/application/", (with) =>
            {
                with.HttpRequest();
                with.FormValue("name", "TestApplication1");
            });

            Assert.That(postResponse.StatusCode, Is.EqualTo(HttpStatusCode.SeeOther));
            Assert.That(postResponse.Headers["Location"], Is.EqualTo("/feature"));
        }

        [Test]
        public void WhenIPostANewApplication_ThenItIsRendered()
        {
            _browser.Post("/application/", (with) =>
            {
                with.HttpRequest();
                with.FormValue("name", "TestApplication1");
            });

            var response = _browser.Get("/feature/");
            Assert.That(response.Body[".application > a"].Any(a => a.InnerText.Contains("TestApplication1")));
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
        private const string ConnectionString = "Lemonade";
    }
}