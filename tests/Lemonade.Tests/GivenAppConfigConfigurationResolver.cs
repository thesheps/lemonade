using System;
using Lemonade.Resolvers;
using NUnit.Framework;

namespace Lemonade.Tests
{
    public class GivenAppConfigConfigurationResolver
    {
        [Test]
        public void WhenIHaveAKnownStringValue_ThenItIsRetrievedCorrectly()
        {
            var resolver = new AppConfigConfigurationResolver();
            var value = resolver.Resolve<string>("TestString", null);
            Assert.That(value, Is.EqualTo("Test String"));
        }

        [Test]
        public void WhenIHaveAKnownBooleanValue_ThenItIsRetrievedCorrectly()
        {
            var resolver = new AppConfigConfigurationResolver();
            var value = resolver.Resolve<bool>("TestBoolean", null);
            Assert.That(value, Is.True);
        }

        [Test]
        public void WhenIHaveAKnownDateValue_ThenItIsRetrievedCorrectly()
        {
            var resolver = new AppConfigConfigurationResolver();
            var value = resolver.Resolve<DateTime>("TestDate", null);
            Assert.That(value, Is.EqualTo(new DateTime(1985, 9, 11, 11, 0, 0)));
        }

        [Test]
        public void WhenIHaveAKnownIntegerValue_ThenItIsRetrievedCorrectly()
        {
            var resolver = new AppConfigConfigurationResolver();
            var value = resolver.Resolve<int>("TestInteger", null);
            Assert.That(value, Is.EqualTo(42));
        }

        [Test]
        public void WhenIHaveAKnownDoubleValue_ThenItIsRetrievedCorrectly()
        {
            var resolver = new AppConfigConfigurationResolver();
            var value = resolver.Resolve<double>("TestDouble", null);
            Assert.That(value, Is.EqualTo(3.142));
        }

        [Test]
        public void WhenIHaveAKnownDecimalValue_ThenItIsRetrievedCorrectly()
        {
            var resolver = new AppConfigConfigurationResolver();
            var value = resolver.Resolve<decimal>("TestDecimal", null);
            Assert.That(value, Is.EqualTo(10.27));
        }

        [Test]
        public void WhenIHaveAKnownUriValue_ThenItIsRetrievedCorrectly()
        {
            var resolver = new AppConfigConfigurationResolver();
            var value = resolver.Resolve<Uri>("TestUri", null);
            Assert.That(value.AbsoluteUri, Is.EqualTo("http://localhost:10547/"));
        }
    }
}