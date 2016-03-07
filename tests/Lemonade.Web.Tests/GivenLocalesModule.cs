using System.Collections.Generic;
using System.Linq;
using Lemonade.Sql.Migrations;
using Lemonade.Web.Contracts;
using Lemonade.Web.Infrastructure;
using Nancy.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using SelfishHttp;

namespace Lemonade.Web.Tests
{
    public class GivenLocalesModule
    {
        [SetUp]
        public void SetUp()
        {
            Runner.SqlCompact(ConnectionString).Down();
            Runner.SqlCompact(ConnectionString).Up();
            _server = new Server(64978);
            _browser = new Browser(new LemonadeBootstrapper(), context => context.UserHostAddress("localhost"));
        }

        [TearDown]
        public void TearDown()
        {
            _server.Dispose();
        }

        [Test]
        public void WhenIGetLocales_ThenTheyAreRetrieved()
        {
            var response = _browser.Get("/api/locales", with =>
            {
                with.Header("Accept", "application/json");
            });

            var result = JsonConvert.DeserializeObject<IList<Locale>>(response.Body.AsString());
            Assert.That(result.Any());
        }

        private Server _server;
        private Browser _browser;
        private const string ConnectionString = "Lemonade";
    }
}