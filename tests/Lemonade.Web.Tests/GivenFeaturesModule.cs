using System;
using System.Collections.Generic;
using System.Data.SQLite.EF6;
using System.Linq;
using Lemonade.Sql;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Queries;
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
            const string connectionString = "FullUri=file::memory:?cache=shared";
            _dbProviderFactory = new SQLiteProviderFactory();

            DbMigrations.Sqlite(connectionString).Up();
            _saveFeature = new SaveFeature(_dbProviderFactory, connectionString);
            _getAllFeatures = new GetAllFeatures(_dbProviderFactory, connectionString);

            _browser = new Browser(new ConfigurableBootstrapper(with =>
            {
                with.Module<FeatureModule>();
                with.Dependency(_getAllFeatures);
                with.Dependency(_saveFeature);
            }));
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
        private SaveFeature _saveFeature;
        private GetAllFeatures _getAllFeatures;
        private SQLiteProviderFactory _dbProviderFactory;
    }
}