using System;
using System.Net;
using Lemonade.Builders;
using Lemonade.Core.Exceptions;
using Lemonade.Data.Commands;
using Lemonade.Fakes;
using Lemonade.Services;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using Lemonade.Web.Infrastructure;
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
            _createFeature = new CreateFeatureFake();
            _createApplication = new CreateApplicationFake();
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

            new CreateFeatureOverrideFake().Execute(new Data.Entities.FeatureOverride { FeatureId = feature2.FeatureId, Hostname = Dns.GetHostName(), IsEnabled = true });
            _nancyHost = new NancyHost(new Uri("http://localhost:12345"), new LemonadeBootstrapper());
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
        public void WhenIHaveAnUnknownFeatureAndITryToRetrieveIt_ThenUnknownFeatureExceptionIsThrown()
        {
            Assert.Throws<FeatureCouldNotBeFoundException>(() =>
            {
                var feature = Feature.Switches["Sheep"];
            });
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

        private ICreateFeature _createFeature;
        private NancyHost _nancyHost;
        private ICreateApplication _createApplication;
        private GetApplicationByName _getApplicationByName;
        private GetFeatureByNameAndApplication _getFeature;
    }
}