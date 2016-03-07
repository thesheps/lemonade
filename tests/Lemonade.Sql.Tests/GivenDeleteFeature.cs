using Lemonade.Builders;
using Lemonade.Fakes;
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
            _createFeature = new CreateFeatureFake();
            _createApplication = new CreateApplicationFake();
            _deleteFeature = new DeleteFeature();
            _getApplicationByName = new GetApplicationByName();
            _getFeatureByNameAndApplication = new GetFeatureByNameAndApplication();
            Runner.SqlCompact("Lemonade").Down();
            Runner.SqlCompact("Lemonade").Up();
        }

        [Test]
        public void WhenIDeleteAFeature_ThenItIsNoLongerAvailable()
        {
            var application = new ApplicationBuilder()
                .WithName("Test12345")
                .Build();

            _createApplication.Execute(application);
            application = _getApplicationByName.Execute(application.Name);

            var feature = new FeatureBuilder()
                .WithName("SuperFeature123")
                .WithApplication(application)
                .Build();

            _createFeature.Execute(feature);
            feature = _getFeatureByNameAndApplication.Execute(feature.Name, application.Name);

            _deleteFeature.Execute(feature.FeatureId);
            feature = _getFeatureByNameAndApplication.Execute(feature.Name, application.Name);

            Assert.That(feature, Is.Null);
        }

        private GetFeatureByNameAndApplication _getFeatureByNameAndApplication;
        private CreateApplicationFake _createApplication;
        private CreateFeatureFake _createFeature;
        private DeleteFeature _deleteFeature;
        private GetApplicationByName _getApplicationByName;
    }
}