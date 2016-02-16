using System;
using System.Net;
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

            Configuration.FeatureResolver = new HttpFeatureResolver("http://localhost:12345");
            Runner.SqlCompact("Lemonade").Down();
            Runner.SqlCompact("Lemonade").Up();
            _getFeature = new GetFeatureByNameAndApplication();
            _createFeature = new CreateFeature();
            _createApplication = new CreateApplication();
            _getApplicationByName = new GetApplicationByName();
            _createApplication.Execute(application);
            application = _getApplicationByName.Execute(application.Name);

            var feature1 = new FeatureBuilder().WithName("MySuperDuperFeature")
                .WithApplication(application)
                .WithIsEnabled(true)
                .Build();

            var feature2 = new FeatureBuilder().WithName("Ponies")
                .WithApplication(application)
                .WithIsEnabled(true)
                .Build();

            _createFeature.Execute(feature1);
            _createFeature.Execute(feature2);

            new CreateFeatureOverride().Execute(new Data.Entities.FeatureOverride { FeatureId = feature2.FeatureId, Hostname = Dns.GetHostName(), IsEnabled = true });
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
            var enabled = Feature.Switches["Sheep"];
            var feature = _getFeature.Execute("Sheep", "Test Application");
            Assert.That(feature.IsEnabled, Is.False);
            Assert.That(enabled, Is.False);
        }

        [Test]
        public void WhenIGetAFeatureWithAHostnameOveride_ThenTheFeatureIsRetrieved()
        {
            Configuration.ApplicationName = "Test Application";
            var enabled = Feature.Switches["Ponies"];
            Assert.That(enabled, Is.True);
        }

        [Test]
        public void WhenIHaveConfiguredAnApplicationName_ThenItIsUsed()
        {
            Assert.That(Configuration.ApplicationName, Is.EqualTo("Test Application"));
        }

        private CreateFeature _createFeature;
        private NancyHost _nancyHost;
        private CreateApplication _createApplication;
        private GetApplicationByName _getApplicationByName;
        private GetFeatureByNameAndApplication _getFeature;
    }
}