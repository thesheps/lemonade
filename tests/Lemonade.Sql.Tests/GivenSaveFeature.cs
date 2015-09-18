using System;
using Lemonade.Builders;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Exceptions;
using Lemonade.Sql.Migrations;
using NUnit.Framework;

namespace Lemonade.Sql.Tests
{
    public class GivenSaveFeature
    {
        [SetUp]
        public void SetUp()
        {
            Runner.Sqlite("Lemonade").Down();
            Runner.Sqlite("Lemonade").Up();
        }

        [Test]
        public void WhenITryToSaveADuplicateFeature_ThenSaveFeatureExceptionIsThrown()
        {
            var saveFeature = new SaveFeature();
            var application = new ApplicationBuilder()
                .WithName("Test12345")
                .Build();

            var feature = new FeatureBuilder()
                .WithName("MyTestFeature")
                .WithStartDate(DateTime.Now)
                .WithApplication(application).Build();

            saveFeature.Execute(feature);

            Assert.Throws<SaveFeatureException>(() => saveFeature.Execute(feature));
        }
    }
}