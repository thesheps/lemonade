using System;
using Lemonade.Data.Entities;
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
            var feature = new Feature { ApplicationName = "Test12345", FeatureName = "MyTestFeature", StartDate = DateTime.Now };
            saveFeature.Execute(feature);

            Assert.Throws<SaveFeatureException>(() => saveFeature.Execute(feature));
        }
    }
}