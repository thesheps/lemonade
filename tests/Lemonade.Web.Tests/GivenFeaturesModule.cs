using System;
using System.Collections.Generic;
using System.Linq;
using Lemonade.Data.Commands;
using Lemonade.Data.Queries;
using Lemonade.Resolvers;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Host;
using Lemonade.Web.Modules;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using SelfishHttp;

namespace Lemonade.Web.Tests
{
    public class GivenFeaturesModule
    {
        [SetUp]
        public void SetUp()
        {
            _server = new Server(64978);
            Runner.Sqlite(ConnectionString).Down();
            Runner.Sqlite(ConnectionString).Up();

            _browser = new Browser(new Bootstrapper());
        }

        [TearDown]
        public void Teardown()
        {
            _server.Dispose();
        }

        [Test]
        public void WhenIPostMultipleFeatures_ThenTheFeaturesAreSavedAndTheSameNumberAreRendered()
        {
            _browser.Post("/api/feature", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(GetFeatureModel("MySuperCoolFeature1")));
            });

            _browser.Post("/api/feature", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(GetFeatureModel("MySuperCoolFeature2")));
            });

            var response = _browser.Get("/features");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Body[".feature"].Count(), Is.EqualTo(2));
        }

        [Test]
        public void WhenIPostMultipleFeatures_ThenTheFeaturesAreSavedAndTheSameNumberAreRetrievedViaRest()
        {
            _browser.Post("/api/feature", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(GetFeatureModel("MySuperCoolFeature1")));
            });

            _browser.Post("/api/feature", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(GetFeatureModel("MySuperCoolFeature2")));
            });

            var response = _browser.Get("/api/features", b => b.Header("Accept", "application/json"));
            var results = JsonConvert.DeserializeObject<IList<Contracts.Feature>>(response.Body.AsString());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(results.Count, Is.EqualTo(2));
        }

        [Test]
        public void WhenIPostAFeature_ThenICanGetItViaHttp()
        {
            _browser.Post("/api/feature", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(GetFeatureModel("MySuperCoolFeature1")));
            });

            var response = _browser.Get("/api/feature", with =>
            {
                with.Header("Accept", "application/json");
                with.Query("application", "TestApplication");
                with.Query("feature", "MySuperCoolFeature1");
            });

            var result = JsonConvert.DeserializeObject<Contracts.Feature>(response.Body.AsString());
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.IsEnabled, Is.True);
        }

        [Test]
        public void WhenIHaveAnUnknownUrlAppConfigAndITryToResolveAFeatureUsingHttpFeatureResolver_ThenUnknownUrlExceptionIsThrown()
        {
            _browser.Post("/api/feature", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(GetFeatureModel("MySuperCoolFeature1")));
            });

            Assert.Throws<UriFormatException>(() => new HttpFeatureResolver("TestTestTest!!!"));
        }

        private static Contracts.Feature GetFeatureModel(string name)
        {
            return new Contracts.Feature
            {
                ExpirationDays = 1,
                IsEnabled = true,
                StartDate = DateTime.Now,
                FeatureName = name,
                ApplicationName = "TestApplication"
            };
        }

        private Browser _browser;
        private Server _server;
        private const string ConnectionString = "Lemonade";
    }
}