using Lemonade.Data.Entities;
using Lemonade.Fakes;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Queries;
using NUnit.Framework;

namespace Lemonade.Sql.Tests
{
    [TestFixture]
    public class GivenCreateFeatureOverride
    {
        [SetUp]
        public void SetUp()
        {
            Migrations.Runner.SqlCompact("Lemonade").Down();
            Migrations.Runner.SqlCompact("Lemonade").Up();
        }

        [Test]
        public void WhenISaveAFeatureOverride_ThenTheFeatureOverrideIsSaved()
        {
            var application = new Application { Name = "Test" };
            new CreateApplicationFake().Execute(application);

            var feature = new Feature { Name = "Test", ApplicationId = application.ApplicationId };
            new CreateFeatureFake().Execute(feature);

            var featureOverride = new FeatureOverride { IsEnabled = true, FeatureId = feature.FeatureId, Hostname = "Test" };
            new CreateFeatureOverrideFake().Execute(featureOverride);

            var features = new GetAllFeaturesByApplicationId().Execute(application.ApplicationId);

            Assert.That(features[0].FeatureOverrides[0].Hostname, Is.EqualTo("Test"));
            Assert.That(features[0].FeatureOverrides[0].IsEnabled, Is.True);
        }
    }
}