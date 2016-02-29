using Lemonade.Services;
using NUnit.Framework;

namespace Lemonade.Tests
{
    public class GivenDefaultFeatureResolver
    {
        [Test]
        public void WhenIHaveAnEnabledKnownFeatureSwitch_ThenTheValueIsRetrievedAsTrue()
        {
            var resolver = new DefaultFeatureResolver();
            var enabled = resolver.Resolve("EnabledProperty", null);
            Assert.That(enabled, Is.True);
        }

        [Test]
        public void WhenIHaveADisabledKnownFeatureSwitch_ThenTheValueIsRetrievedAsFalse()
        {
            var resolver = new DefaultFeatureResolver();
            var enabled = resolver.Resolve("DisabledProperty", null);
            Assert.That(enabled, Is.False);
        }

        [Test]
        public void WhenIHaveAnUnknownFeatureSwitch_ThenTheValueIsRetrievedAsFalse()
        {
            var resolver = new DefaultFeatureResolver();
            var enabled = resolver.Resolve("NullProperty", null);
            Assert.That(enabled, Is.False);
        }

        [Test]
        public void WhenIHaveAnEnabledKnownAppConfigSettingAndUseFeatureWrapper_ThenTheValueIsRetrievedAsTrue()
        {
            Configuration.FeatureResolver = new DefaultFeatureResolver();
            Assert.That(Feature.Switches["EnabledProperty"], Is.True);
            Assert.That(Feature.Switches[d => d.EnabledProperty], Is.True);
        }

        [Test]
        public void WhenIHaveAKnownFeatureSwitchAndUseXmlConfiguration_ThenTheValueIsRetrievedAsTrue()
        {
            Assert.That(Feature.Switches["EnabledProperty"], Is.True);
            Assert.That(Feature.Switches[d => d.EnabledProperty], Is.True);
        }
    }
}