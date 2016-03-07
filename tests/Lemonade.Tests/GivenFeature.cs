using System;
using System.Threading;
using Lemonade.Core.Services;
using Lemonade.Services;
using NSubstitute;
using NUnit.Framework;

namespace Lemonade.Tests
{
    public class GivenFeature
    {
        private IFeatureResolver _featureResolver;

        [SetUp]
        public void Setup()
        {
            _featureResolver = Substitute.For<IFeatureResolver>();
            _featureResolver.Resolve("UseTestFunctionality", Arg.Any<string>()).Returns(true);
            _featureResolver.Resolve("Test1", Arg.Any<string>()).Returns(false);
            _featureResolver.Resolve("Test2", Arg.Any<string>()).Returns(false);

            Configuration.FeatureResolver = _featureResolver;
            Configuration.CacheProvider = new DefaultCacheProvider(new DefaultRetryPolicy(3), 0.0);
        }

        [Test]
        public void WhenUsingFeatureIndexAndMethodIsFeatureSwitchedOn_ThenItIsExecuted()
        {
            var executed = Feature.Switches["UseTestFunctionality"];
            Assert.That(executed, Is.True);
        }

        [Test]
        public void WhenUsingDynamicIndexAndMethodIsFeatureSwitchedOn_ThenItIsExecuted()
        {
            var executed = Feature.Switches[d => d.UseTestFunctionality];
            Assert.That(executed, Is.True);
        }

        [Test]
        public void WhenUsingTypedIndexAndMethodIsFeatureSwitchedOn_ThenItIsExecuted()
        {
            var executed = Feature.Switches.Get<TestFeatures>(t => t.UseTestFunctionality);
            Assert.That(executed, Is.True);
        }

        [Test]
        public void WhenUsingFeatureWrapperwithIndexAndMethodIsFeatureSwitchedOn_ThenItIsExecuted()
        {
            var executed = false;
            Feature.Switches.Execute("UseTestFunctionality", () => executed = true);
            Assert.That(executed, Is.True);
        }

        [Test]
        public void WhenUsingFeatureWrapperwithDynamicIndexAndMethodIsFeatureSwitchedOn_ThenItIsExecuted()
        {
            var executed = false;
            Feature.Switches.Execute(d => d.UseTestFunctionality, () => executed = true);
            Assert.That(executed, Is.True);
        }

        [Test]
        public void WhenUsingDynamicFeatureWrapperAndMethodIsFeatureSwitchedOn_ThenItIsExecuted()
        {
            var executed = false;
            Feature.Switches.Execute(d => d.UseTestFunctionality, () => executed = true);
            Assert.That(executed, Is.True);
        }

        [Test]
        public void WhenUsingCacheExpiration_ThenCacheIsRefreshedAfterSpecifiedTime()
        {
            var enabled = false;
            Configuration.CacheProvider = new DefaultCacheProvider(new DefaultRetryPolicy(3), 0.1);
            enabled = Feature.Switches["Test1"];
            enabled = Feature.Switches["Test1"];
            enabled = Feature.Switches["Test1"];
            enabled = Feature.Switches["Test1"];
            enabled = Feature.Switches["Test1"];
            enabled = Feature.Switches["Test1"];
            Thread.Sleep(TimeSpan.FromSeconds(10));
            enabled = Feature.Switches["Test1"];

            _featureResolver.Received(2).Resolve("Test1", Arg.Any<string>());
        }

        [Test]
        public void WhenUsingNoCacheExpiration_ThenCacheIsRefreshedAways()
        {
            bool enabled;
            Configuration.CacheProvider = new DefaultCacheProvider(new DefaultRetryPolicy(3), 0);
            enabled = Feature.Switches["Test2"];
            enabled = Feature.Switches["Test2"];
            enabled = Feature.Switches["Test2"];
            enabled = Feature.Switches["Test2"];
            enabled = Feature.Switches["Test2"];
            enabled = Feature.Switches["Test2"];
            enabled = Feature.Switches["Test2"];

            _featureResolver.Received(7).Resolve("Test2", Arg.Any<string>());
        }
    }

    public class TestFeatures
    {
        public bool UseTestFunctionality { get; set; }
    }
}