using Lemonade.Services;
using NUnit.Framework;

namespace Lemonade.Tests
{
    public class GivenAppConfigFeatureResolver
    {
        [Test]
        public void WhenIHaveAnEnabledKnownAppConfigSetting_ThenTheValueIsRetrievedAsTrue()
        {
            var resolver = new AppConfigFeatureResolver();
            var enabled = resolver.Get("EnabledProperty");
            Assert.That(enabled, Is.True);
        }

        [Test]
        public void WhenIHaveADisabledKnownAppConfigSetting_ThenTheValueIsRetrievedAsFalse()
        {
            var resolver = new AppConfigFeatureResolver();
            var enabled = resolver.Get("DisabledProperty");
            Assert.That(enabled, Is.False);
        }

        [Test]
        public void WhenIHaveAnUnknownAppConfigSetting_ThenTheValueIsRetrievedAsNull()
        {
            var resolver = new AppConfigFeatureResolver();
            var enabled = resolver.Get("NullProperty");
            Assert.That(enabled, Is.Null);
        }

        [Test]
        public void WhenIHaveAnEnabledKnownAppConfigSettingAndUseFeatureWrapper_ThenTheValueIsRetrievedAsTrue()
        {
            Feature.SetResolver(new AppConfigFeatureResolver());
            Assert.That(Feature.Switches["EnabledProperty"], Is.True);
            Assert.That(Feature.Switches[d => d.EnabledProperty], Is.True);
        }
    }
}