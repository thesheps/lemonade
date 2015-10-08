using System;
using System.Threading;
using Lemonade.Resolvers;
using Lemonade.Tests.Fakes;
using NUnit.Framework;

namespace Lemonade.Tests
{
    public class GivenFeatureSwitches : IFeatureResolver
    {
        private int _attempts;

        [Test]
        public void WhenUsingFeatureIndexAndMethodIsFeatureSwitchedOn_ThenItIsExecuted()
        {
            Feature.Resolver = new FakeResolver();
            var executed = Feature.Switches["UseTestFunctionality"];
            Assert.That(executed, Is.True);
        }

        [Test]
        public void WhenUsingDynamicIndexAndMethodIsFeatureSwitchedOn_ThenItIsExecuted()
        {
            Feature.Resolver = new FakeResolver();
            var executed = Feature.Switches[d => d.UseTestFunctionality];
            Assert.That(executed, Is.True);
        }

        [Test]
        public void WhenUsingTypedIndexAndMethodIsFeatureSwitchedOn_ThenItIsExecuted()
        {
            Feature.Resolver = new FakeResolver();
            var executed = Feature.Switches.Get<TestFeatures>(t => t.UseTestFunctionality);
            Assert.That(executed, Is.True);
        }

        [Test]
        public void WhenUsingFeatureWrapperwithIndexAndMethodIsFeatureSwitchedOn_ThenItIsExecuted()
        {
            var executed = false;
            Feature.Resolver = new FakeResolver();
            Feature.Switches.Execute("UseTestFunctionality", () => executed = true);
            Assert.That(executed, Is.True);
        }

        [Test]
        public void WhenUsingFeatureWrapperwithDynamicIndexAndMethodIsFeatureSwitchedOn_ThenItIsExecuted()
        {
            var executed = false;
            Feature.Resolver = new FakeResolver();
            Feature.Switches.Execute(d => d.UseTestFunctionality, () => executed = true);
            Assert.That(executed, Is.True);
        }

        [Test]
        public void WhenUsingDynamicFeatureWrapperAndMethodIsFeatureSwitchedOn_ThenItIsExecuted()
        {
            var executed = false;
            Feature.Resolver = new FakeResolver();
            Feature.Switches.Execute(d => d.UseTestFunctionality, () => executed = true);
            Assert.That(executed, Is.True);
        }

        [Test]
        public void WhenUsingCacheExpiration_ThenCacheIsRefreshedAfterAMinute()
        {
            bool enabled;
            Feature.Resolver = this;
            enabled = Feature.Switches["Test"];
            enabled = Feature.Switches["Test"];
            Thread.Sleep(TimeSpan.FromMinutes(1));
            enabled = Feature.Switches["Test"];

            Assert.That(_attempts == 2);
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