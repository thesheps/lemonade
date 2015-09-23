using System;
using Lemonade.Builders;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using NUnit.Framework;

namespace Lemonade.Sql.Tests
{
    public class GivenDeleteFeature
    {
        [SetUp]
        public void SetUp()
        {
            _saveFeature = new SaveFeature();
            _saveApplication = new SaveApplication();
            _deleteFeature = new DeleteFeature();
            _getApplicationByName = new GetApplicationByName();
            _getFeatureByNameAndApplication = new GetFeatureByNameAndApplication();
            Runner.SqlCompact("Lemonade").Down();
            Runner.SqlCompact("Lemonade").Up();
        }

        [Test]
        public void WhenIDeleteAFeatures_ThenItIsNoLongerAvailable()
        {
            var application = new ApplicationBuilder()
                .WithName("Test12345")
                .Build();

            _saveApplication.Execute(application);
            application = _getApplicationByName.Execute(application.Name);

            var feature = new FeatureBuilder()
                .WithName("SuperFeature123")
                .WithApplication(application)
                .WithStartDate(DateTime.Now)
                .Build();

            _saveFeature.Execute(feature);
            feature = _getFeatureByNameAndApplication.Execute(feature.Name, application.Name);

            _deleteFeature.Execute(feature.FeatureId);
            feature = _getFeatureByNameAndApplication.Execute(feature.Name, application.Name);

            Assert.That(feature, Is.Null);
        }

        private GetFeatureByNameAndApplication _getFeatureByNameAndApplication;
        private SaveApplication _saveApplication;
        private SaveFeature _saveFeature;
        private DeleteFeature _deleteFeature;
        private GetApplicationByName _getApplicationByName;
    }
}