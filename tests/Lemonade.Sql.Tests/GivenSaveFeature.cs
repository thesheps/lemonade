using System;
using Lemonade.Builders;
using Lemonade.Core.Exceptions;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using NUnit.Framework;

namespace Lemonade.Sql.Tests
{
    public class GivenSaveFeature
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
            var saveFeature = new SaveFeature();
            var saveApplication = new SaveApplication();
            var getApplicationByName = new GetApplicationByName();

            var application = new ApplicationBuilder()
                .WithName("Test12345")
                .Build();

            saveApplication.Execute(application);
            application = getApplicationByName.Execute(application.Name);

            var feature = new FeatureBuilder()
                .WithName("MyTestFeature")
                .WithStartDate(DateTime.Now)
                .WithApplication(application).Build();

            saveFeature.Execute(feature);

            Assert.Throws<SaveFeatureException>(() => saveFeature.Execute(feature));
        }
    }
}