using System;
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
        [Test]
        public void WhenIPostMultipleFeatures_ThenTheFeaturesAreSaved()
        {
            const string connectionString = "FullUri=file::memory:?cache=shared";
            var dbProviderFactory = new SQLiteProviderFactory();

            DbMigrations.Sqlite(connectionString).Up();
            var saveFeature = new SaveFeature(dbProviderFactory, connectionString);
            var getAllFeatures = new GetAllFeatures(dbProviderFactory, connectionString);

            var browser = new Browser(new ConfigurableBootstrapper(with =>
            {
                with.Module<FeaturesModule>();
                with.Dependency(getAllFeatures);
                with.Dependency(saveFeature);
            }));

            browser.Post("/features", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(GetFeatureModel("MySuperCoolFeature1")));
            });

            browser.Post("/features", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(GetFeatureModel("MySuperCoolFeature2")));
            });

            var response = browser.Get("/features");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Body[".feature"].Count(), Is.EqualTo(2));
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
    }
}