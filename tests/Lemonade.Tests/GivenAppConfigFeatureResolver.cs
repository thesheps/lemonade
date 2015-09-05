using NUnit.Framework;

namespace Lemonade.Tests
{
    public class GivenAppConfigFeatureResolver
    {
        [Test]
        public void WhenIHaveAnEnabledKnownFeatureSwitch_ThenTheValueIsRetrievedAsTrue()
        {
            var resolver = new AppConfigFeatureResolver();
            var enabled = resolver.Get("EnabledProperty");
            Assert.That(enabled, Is.True);
        }

        [Test]
        public void WhenIHaveADisabledKnownFeatureSwitch_ThenTheValueIsRetrievedAsFalse()
        {
            var resolver = new AppConfigFeatureResolver();
            var enabled = resolver.Get("DisabledProperty");
            Assert.That(enabled, Is.False);
        }

        [Test]
        public void WhenIHaveAnUnknownFeatureSwitch_ThenTheValueIsRetrievedAsNull()
        {
            var resolver = new AppConfigFeatureResolver();
            var enabled = resolver.Get("NullProperty");
            Assert.That(enabled, Is.Null);
        }

        [Test]
        public void WhenIHaveAnEnabledKnownAppConfigSettingAndUseFeatureWrapper_ThenTheValueIsRetrievedAsTrue()
        {
            Feature.Resolver(new AppConfigFeatureResolver());
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