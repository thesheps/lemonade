using System;
using Lemonade.Builders;
using Lemonade.Core.Commands;
using Lemonade.Core.Queries;
using Lemonade.Resolvers;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using Lemonade.Web.Infrastructure;
using Microsoft.AspNet.SignalR;
using Nancy.Hosting.Self;
using Nancy.TinyIoc;
using NUnit.Framework;

namespace Lemonade.AcceptanceTests
{
    public class GivenHttpFeatureResolver
    {
        [SetUp]
        public void SetUp()
        {
            var application = new ApplicationBuilder().WithName(AppDomain.CurrentDomain.FriendlyName).Build();

            Feature.Resolver = new HttpFeatureResolver("http://localhost:12345");
            Runner.SqlCompact("Lemonade").Down();
            Runner.SqlCompact("Lemonade").Up();
            _getFeature = new GetFeatureByNameAndApplication();
            _saveFeature = new SaveFeature();
            _saveApplication = new SaveApplication();
            _getApplicationByName = new GetApplicationByName();
            _saveApplication.Execute(application);
            application = _getApplicationByName.Execute(application.Name);

            var feature = new FeatureBuilder().WithName("MySuperDuperFeature")
                .WithApplication(application)
                .WithIsEnabled(true)
                .WithStartDate(DateTime.Now)
                .Build();

            _saveFeature.Execute(feature);
            _nancyHost = new NancyHost(new Uri("http://localhost:12345"), new TestBootstrapper());
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
        public void WhenIHaveAnUnknownFeatureAndITryToRetrieveIt_ThenItIsFalse()
        {
            var enabled = Feature.Switches["Ponies"];
            Assert.That(enabled, Is.False);
        }

        [Test]
        public void WhenIHaveAnUnknownFeatureAndITryToRetrieveIt_ThenItIsInserted()
        {
            var enabled = Feature.Switches["Ponies"];
            var feature = _getFeature.Execute("Ponies", AppDomain.CurrentDomain.FriendlyName);
            Assert.That(feature.IsEnabled, Is.False);
        }

        [Test]
        public void WhenIHaveConfiguredAnApplicationName_ThenItIsUsed()
        {
            Assert.That(Feature.ApplicationName, Is.EqualTo("Test Application"));
        }

        private class TestBootstrapper : LemonadeBootstrapper
        {
            protected override void ConfigureDependencies(TinyIoCContainer container)
            {
                container.Register(GlobalHost.ConnectionManager);
                container.Register<IGetAllFeatures, GetAllFeatures>();
                container.Register<IGetFeatureByNameAndApplication, GetFeatureByNameAndApplication>();
                container.Register<ISaveFeature, SaveFeature>();
            }
        }

        private SaveFeature _saveFeature;
        private NancyHost _nancyHost;
        private SaveApplication _saveApplication;
        private GetApplicationByName _getApplicationByName;
        private GetFeatureByNameAndApplication _getFeature;
    }
}