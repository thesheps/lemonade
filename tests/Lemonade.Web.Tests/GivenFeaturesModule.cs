using System;
using System.Collections.Generic;
using System.Data.SQLite.EF6;
using System.Linq;
using Lemonade.Data.Commands;
using Lemonade.Data.Queries;
using Lemonade.Sql;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Queries;
using Lemonade.Web.Models;
using Lemonade.Web.Modules;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;

namespace Lemonade.Web.Tests
{
    public class GivenFeaturesModule
    {
        [Test]
        public void WhenIGetFeaturesAndThreeAreAvailable_ThenViewIsFoundAndAllFeaturesAreRendered()
        {
            var getAllFeatures = Substitute.For<IGetAllFeatures>();
            getAllFeatures.Execute().Returns(new List<Data.Entities.Feature>
            {
                new Data.Entities.Feature(),
                new Data.Entities.Feature(),
                new Data.Entities.Feature()
            });

            var browser = new Browser(new ConfigurableBootstrapper(with =>
            {
                with.Module<FeaturesModule>();
                with.Dependency(getAllFeatures);
                with.Dependency(Substitute.For<ISaveFeature>());
            }));

            var response = browser.Get("/features");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Body[".feature"].Count(), Is.EqualTo(3));
        }

        [Test]
        public void WhenIPostAFeature_ThenTheFeatureIsSaved()
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
                with.Body(JsonConvert.SerializeObject(new FeatureModel
                {
                    ExpirationDays = 1,
                    IsEnabled = true,
                    StartDate = DateTime.Now,
                    Name = "MySuperCoolFeature",
                    Application = "TestApplication"
                }));
            });

            Assert.That(getAllFeatures.Execute().Count(), Is.EqualTo(1));
        }
    }
}