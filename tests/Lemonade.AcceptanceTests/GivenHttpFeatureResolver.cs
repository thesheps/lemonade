using System;
using Lemonade.Data.Commands;
using Lemonade.Data.Queries;
using Lemonade.Resolvers;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using Nancy;
using Nancy.Bootstrapper;
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
            Feature.Resolver = new HttpFeatureResolver("http://localhost:12345");
            Runner.Sqlite("Lemonade").Down();
            Runner.Sqlite("Lemonade").Up();
            _saveFeature = new SaveFeature();
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
            _saveFeature.Execute(new Data.Entities.Feature
            {
                ApplicationName = AppDomain.CurrentDomain.FriendlyName,
                FeatureName = "MySuperDuperFeature",
                IsEnabled = true
            });

            var enabled = Feature.Switches["MySuperDuperFeature"];
            Assert.That(enabled, Is.True);
        }

        [Test]
        public void WhenIHaveAnUnknownFeatureAndITryToRetrieveIt_ThenItIsFalse()
        {
            _saveFeature.Execute(new Data.Entities.Feature
            {
                ApplicationName = AppDomain.CurrentDomain.FriendlyName,
                FeatureName = "MySuperDuperFeature",
                IsEnabled = true
            });

            var enabled = Feature.Switches["Ponies"];
            Assert.That(enabled, Is.False);
        }

        private class TestBootstrapper : DefaultNancyBootstrapper
        {
            protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
            {
                base.ApplicationStartup(container, pipelines);
                container.Register<IGetAllFeatures, GetAllFeatures>();
                container.Register<IGetFeatureByNameAndApplication, GetFeatureByNameAndApplication>();
                container.Register<ISaveFeature, SaveFeature>();
            }
        }

        private SaveFeature _saveFeature;
        private NancyHost _nancyHost;
    }
}