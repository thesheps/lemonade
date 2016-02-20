using System;
using Lemonade.Core.Services;
using Lemonade.Resolvers;
using Lemonade.Services;
using NUnit.Framework;

namespace Lemonade.Tests
{
    public class GivenConfig : IConfigurationResolver
    {
        [Test]
        public void WhenIGetAKnownConfigurationString_ThenTheValueIsRetrieved()
        {
            Configuration.ConfigurationResolver = this;
            Assert.That(Config.Settings<string>()["TestString"], Is.EqualTo("Test String"));
        }

        [Test]
        public void WhenIGetAKnownConfigurationBoolean_ThenTheValueIsRetrieved()
        {
            Configuration.ConfigurationResolver = this;
            Assert.That(Config.Settings<bool>()["TestBoolean"], Is.EqualTo(true));
        }

        [Test]
        public void WhenNoConfigurationResolverIsSet_ThenAppConfigConfigurationResolverIsUsed()
        {
            Configuration.ConfigurationResolver = null;
            Assert.That(Configuration.ConfigurationResolver, Is.TypeOf<AppSettingsConfigurationResolver>());
        }

        public T Resolve<T>(string configurationName, string applicationName)
        {
            if (configurationName == "TestString") return (T)Convert.ChangeType("Test String", TypeCode.String);

            return (T)Convert.ChangeType(true, TypeCode.Boolean);
        }
    }
}