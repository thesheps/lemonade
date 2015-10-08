using System;
using Lemonade.Builders;
using Lemonade.Resolvers;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using Nancy.Hosting.Self;
using NUnit.Framework;

namespace Lemonade.AcceptanceTests
{
    public class GivenHttpFeatureResolver
    {
        [SetUp]
        public void SetUp()
        {
            var application = new ApplicationBuilder().WithName("Test Application").Build();

            Feature.Resolver = new HttpFeatureResolver("http://localhost:12345");
            Runner.SqlCompact("Lemonade").Down();
            Runner.SqlCompact("Lemonade").Up();
            _getFeature = new GetFeatureByNameAndApplication();
            _createFeature = new CreateFeature();
            _createApplication = new CreateApplication();
            _getApplicationByName = new GetApplicationByName();
            _createApplication.Execute(application);
            application = _getApplicationByName.Execute(application.Name);

            var feature = new FeatureBuilder().WithName("MySuperDuperFeature")
                .WithApplication(application)
                .WithIsEnabled(true)
                .WithStartDate(DateTime.Now)
                .Build();

            _createFeature.Execute(feature);
            _nancyHost = new NancyHost(new Uri("http://localhost:12345"), new TestLemonadeBootstrapper());
            _nancyHost.Start();
        }

        [TearDown]
        public void TearDown()
        {
            _nancyHost.Stop();
            _nancyHost.Dispose();
        }

        [Test]
        public void WhenIHaveAKnownFeatureAndITryToRetrieveIt_ThenItIsTrue()
        {
            var enabled = Feature.Switches["MySuperDuperFeature"];
            Assert.That(enabled, Is.True);
        }

        [Test]
        public void WhenIHaveAnUnknownFeatureAndITryToRetrieveIt_ThenItIsInserted()
        {
            var enabled = Feature.Switches["Ponies"];
            var feature = _getFeature.Execute("Ponies", "Test Application");
            Assert.That(feature.IsEnabled, Is.False);
            Assert.That(enabled, Is.False);
        }

        [Test]
        public void WhenIHaveConfiguredAnApplicationName_ThenItIsUsed()
        {
            Assert.That(Feature.ApplicationName, Is.EqualTo("Test Application"));
        }

        private CreateFeature _createFeature;
        private NancyHost _nancyHost;
        private CreateApplication _createApplication;
        private GetApplicationByName _getApplicationByName;
        private GetFeatureByNameAndApplication _getFeature;
    }
}