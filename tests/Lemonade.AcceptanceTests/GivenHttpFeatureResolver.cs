using System;
using Lemonade.AcceptanceTests.Helpers;
using Lemonade.Resolvers;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using Lemonade.Web;
using NUnit.Framework;

namespace Lemonade.AcceptanceTests
{
    public class GivenHttpFeatureResolver
    {
        [SetUp]
        public void SetUp()
        {
            var uri = IpHelper.GetLocalIpAddress("12345");
            Feature.Resolver = new HttpFeatureResolver(uri);
            Runner.Sqlite("Lemonade").Down();
            Runner.Sqlite("Lemonade").Up();
            _saveFeature = new SaveFeature();
            _lemonadeService = new LemonadeService(uri, new GetAllFeatures(), new GetFeatureByNameAndApplication(), _saveFeature);
            _lemonadeService.Start();
        }

        [TearDown]
        public void TearDown()
        {
            _lemonadeService.Dispose();
        }

        [Test]
        public void WhenIHaveAKnownFeatureAndITryToRetrieveIt_ThenItIsTrue()
        {
            _saveFeature.Execute(new Data.Entities.Feature
            {
                ApplicationName = AppDomain.CurrentDomain.FriendlyName,
                FeatureName = "MySuperDuperFeature",
                IsEnabled = true
            });

            var enabled = Feature.Switches["MySuperDuperFeature"];
            Assert.That(enabled, Is.True);
        }

        [Test]
        public void WhenIHaveAnUnknownFeatureAndITryToRetrieveIt_ThenItIsFalse()
        {
            _saveFeature.Execute(new Data.Entities.Feature
            {
                ApplicationName = AppDomain.CurrentDomain.FriendlyName,
                FeatureName = "MySuperDuperFeature",
                IsEnabled = true
            });

            var enabled = Feature.Switches["Ponies"];
            Assert.That(enabled, Is.False);
        }

        private LemonadeService _lemonadeService;
        private SaveFeature _saveFeature;
    }
}