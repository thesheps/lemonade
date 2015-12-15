using System;
using Lemonade.Resolvers;
using NUnit.Framework;

namespace Lemonade.Tests
{
    public class GivenConfig : IConfigurationResolver
    {
        [Test]
        public void WhenIGetAKnownConfigurationString_ThenTheValueIsRetrieved()
        {
            Lemonade.ConfigurationResolver = this;
            Assert.That(Config.Settings<string>("TestString"), Is.EqualTo("Test String"));
        }

        [Test]
        public void WhenIGetAKnownConfigurationBoolean_ThenTheValueIsRetrieved()
        {
            Lemonade.ConfigurationResolver = this;
            Assert.That(Config.Settings<bool>("TestBoolean"), Is.EqualTo(true));
        }

        [Test]
        public void WhenNoConfigurationResolverIsSet_ThenAppConfigConfigurationResolverIsUsed()
        {
            Lemonade.ConfigurationResolver = null;
            Assert.That(Lemonade.ConfigurationResolver, Is.TypeOf<AppConfigConfigurationResolver>());
        }

        public T Resolve<T>(string key, string applicationName)
        {
            if (key == "TestString") return (T)Convert.ChangeType("Test String", TypeCode.String);

            return (T)Convert.ChangeType(true, TypeCode.Boolean);
        }
    }
}