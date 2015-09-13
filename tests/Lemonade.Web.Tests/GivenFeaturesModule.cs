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
            Runner.Sqlite(ConnectionString).Up();

            _browser = new Browser(new ConfigurableBootstrapper(with =>
            {
                with.Module<FeatureModule>();
                with.Dependencies<IGetAllFeatures>(new GetAllFeatures());
                with.Dependencies<IGetFeatureByNameAndApplication>(new GetFeatureByNameAndApplication());
                with.Dependencies<ISaveFeature>(new SaveFeature());
            }));
        }

        [TearDown]
        public void Teardown()
        {
            _server.Dispose();
            Runner.Sqlite(ConnectionString).Down();
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

            var response = _browser.Get("/api/feature", with =>
            {
                with.Header("Accept", "application/json");
                with.Query("application", "TestApplication");
                with.Query("feature", "MySuperCoolFeature1");
            });

            var result = JsonConvert.DeserializeObject<FeatureModel>(response.Body.AsString());
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

        [Test]
        public void WhenIHaveAKnownFeatureAndITryToResolveAFeatureUsingHttpFeatureResolver_ThenTheFeatureIsResolved()
        {
            var featureModel = GetFeatureModel("MySuperCoolFeature1");
            var featureResolver = new HttpFeatureResolver();
            _server.OnGet("/api/feature").RespondWith(JsonConvert.SerializeObject(featureModel));
            Assert.That(featureResolver.Get("MySuperCoolFeature1"), Is.True);
        }

        [Test]
        public void WhenIHaveAnUnknownFeatureAndITryToResolveTheFeatureUsingHttpFeatureResolver_ThenTheFeatureIsFalse()
        {
            var featureModel = GetFeatureModel("MySuperCoolFeature1");
            var featureResolver = new HttpFeatureResolver();
            _server.OnGet("/api/feature").RespondWith(JsonConvert.SerializeObject(featureModel));
            Assert.That(featureResolver.Get("MySuperCoolFeature2"), Is.True);
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
        private Server _server;
        private const string ConnectionString = "Data Source=test.db";
    }
}