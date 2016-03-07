using Lemonade.Builders;
using Lemonade.Data.Exceptions;
using Lemonade.Fakes;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using NUnit.Framework;

namespace Lemonade.Sql.Tests
{
    public class GivenCreateFeature
    {
        [SetUp]
        public void SetUp()
        {
            Runner.SqlCompact("Lemonade").Down();
            Runner.SqlCompact("Lemonade").Up();
        }

        [Test]
        public void WhenITryToSaveADuplicateFeature_ThenSaveFeatureExceptionIsThrown()
        {
            var saveFeature = new CreateFeatureFake();
            var saveApplication = new CreateApplicationFake();
            var getApplicationByName = new GetApplicationByName();

            var application = new ApplicationBuilder()
                .WithName("Test12345")
                .Build();

            saveApplication.Execute(application);
            application = getApplicationByName.Execute(application.Name);

            var feature = new FeatureBuilder()
                .WithName("MyTestFeature")
                .WithApplication(application).Build();

            saveFeature.Execute(feature);

            Assert.Throws<CreateFeatureException>(() => saveFeature.Execute(feature));
        }
    }
}