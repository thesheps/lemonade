using System;
using System.Threading;
using Lemonade.Resolvers.Fakes;
using NUnit.Framework;

namespace Lemonade.Resolvers
{
    public class GivenFeature : IFeatureResolver
    {
        private int _attempts;

        [Test]
        public void WhenUsingFeatureIndexAndMethodIsFeatureSwitchedOn_ThenItIsExecuted()
        {
            Lemonade.FeatureResolver = new FakeResolver();
            var executed = Feature.Switches["UseTestFunctionality"];
            Assert.That(executed, Is.True);
        }

        [Test]
        public void WhenUsingDynamicIndexAndMethodIsFeatureSwitchedOn_ThenItIsExecuted()
        {
            Lemonade.FeatureResolver = new FakeResolver();
            var executed = Feature.Switches[d => d.UseTestFunctionality];
            Assert.That(executed, Is.True);
        }

        [Test]
        public void WhenUsingTypedIndexAndMethodIsFeatureSwitchedOn_ThenItIsExecuted()
        {
            Lemonade.FeatureResolver = new FakeResolver();
            var executed = Feature.Switches.Get<TestFeatures>(t => t.UseTestFunctionality);
            Assert.That(executed, Is.True);
        }

        [Test]
        public void WhenUsingFeatureWrapperwithIndexAndMethodIsFeatureSwitchedOn_ThenItIsExecuted()
        {
            var executed = false;
            Lemonade.FeatureResolver = new FakeResolver();
            Feature.Switches.Execute("UseTestFunctionality", () => executed = true);
            Assert.That(executed, Is.True);
        }

        [Test]
        public void WhenUsingFeatureWrapperwithDynamicIndexAndMethodIsFeatureSwitchedOn_ThenItIsExecuted()
        {
            var executed = false;
            Lemonade.FeatureResolver = new FakeResolver();
            Feature.Switches.Execute(d => d.UseTestFunctionality, () => executed = true);
            Assert.That(executed, Is.True);
        }

        [Test]
        public void WhenUsingDynamicFeatureWrapperAndMethodIsFeatureSwitchedOn_ThenItIsExecuted()
        {
            var executed = false;
            Lemonade.FeatureResolver = new FakeResolver();
            Feature.Switches.Execute(d => d.UseTestFunctionality, () => executed = true);
            Assert.That(executed, Is.True);
        }

        [Test]
        public void WhenUsingCacheExpiration_ThenCacheIsRefreshedAfterAMinute()
        {
            bool enabled;
            _attempts = 0;
            Lemonade.CacheExpiration = 1;
            Lemonade.FeatureResolver = this;
            enabled = Feature.Switches["Test"];
            enabled = Feature.Switches["Test"];
            enabled = Feature.Switches["Test"];
            enabled = Feature.Switches["Test"];
            enabled = Feature.Switches["Test"];
            enabled = Feature.Switches["Test"];
            Thread.Sleep(TimeSpan.FromSeconds(61));
            enabled = Feature.Switches["Test"];

            Assert.That(_attempts, Is.EqualTo(2));
        }

        [Test]
        public void WhenUsingNoCacheExpiration_ThenCacheIsRefreshedAways()
        {
            bool enabled;
            _attempts = 0;
            Lemonade.CacheExpiration = 0;
            Lemonade.FeatureResolver = this;
            enabled = Feature.Switches["Test"];
            enabled = Feature.Switches["Test"];
            enabled = Feature.Switches["Test"];
            enabled = Feature.Switches["Test"];
            enabled = Feature.Switches["Test"];
            enabled = Feature.Switches["Test"];
            enabled = Feature.Switches["Test"];

            Assert.That(_attempts, Is.EqualTo(7));
        }

        public bool Resolve(string featureName, string applicationName)
        {
            _attempts++;
            return false;
        }
    }

    public class TestFeatures
    {
        public bool UseTestFunctionality { get; set; }
    }
}