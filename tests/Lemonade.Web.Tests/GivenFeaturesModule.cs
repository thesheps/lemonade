using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite.EF6;
using System.Linq;
using Lemonade.Sql;
using Lemonade.Web.Models;
using Lemonade.Web.Modules;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Lemonade.Web.Tests
{
    public class GivenFeaturesModule
    {
        [SetUp]
        public void SetUp()
        {
            DbMigrations.Sqlite(ConnectionString).Up();

            _browser = new Browser(new ConfigurableBootstrapper(with =>
            {
                with.Module<FeatureModule>();
            }));
        }

        [TearDown]
        public void Teardown()
        {
            DbMigrations.Sqlite(ConnectionString).Down();
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

            var response = _browser.Get("/feature");
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

            var response = _browser.Get("/api/feature", b => b.Header("Accept", "application/json"));
            var results = JsonConvert.DeserializeObject<IList<FeatureModel>>(response.Body.AsString());

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

            var response = _browser.Get("/api/feature", b => b.Header("Accept", "application/json"));
            var results = JsonConvert.DeserializeObject<IList<FeatureModel>>(response.Body.AsString());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(results.Count, Is.EqualTo(1));
        }

        private static FeatureModel GetFeatureModel(string name)
        {
            return new FeatureModel
            {
                ExpirationDays = 1,
                IsEnabled = true,
                StartDate = DateTime.Now,
                FeatureName = name,
                ApplicationName = "TestApplication"
            };
        }

        private Browser _browser;
        private const string ConnectionString = "Data Source=temp.db";
    }
}