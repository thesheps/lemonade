using System;
using System.Threading;
using Lemonade.Services;
using NUnit.Framework;

namespace Lemonade.Tests
{
    public class GivenCacheProvider
    {
        [Test]
        public void WhenTheValueIsNotCached_ThenItIsRetrievedUsingTheSpecifiedStrategy()
        {
            var strategyIsUsed = false;
            var cacheProvider = new CacheProvider(10);
            var value = cacheProvider.GetValue("TEST", () =>
            {
                strategyIsUsed = true;
                return true;
            });

            Assert.That(strategyIsUsed, Is.True);
            Assert.That(value, Is.True);
        }

        [Test]
        public void WhenTheValueIsAlreadyCached_ThenItIsNotRetrievedASecondTime()
        {
            var strategyIsUsed = false;
            var cacheProvider = new CacheProvider(10);
            var value = cacheProvider.GetValue("TEST", () => false);
            value = cacheProvider.GetValue("TEST", () =>
            {
                strategyIsUsed = true;
                return true;
            });

            Assert.That(strategyIsUsed, Is.False);
            Assert.That(value, Is.False);
        }

        [Test]
        public void WhenTheCacheExpirationPeriodHasExpired_ThenTheValueIsRetrievedUsingTheStrategt()
        {
            var strategyIsUsed = false;
            var cacheProvider = new CacheProvider(0.1);
            var value = cacheProvider.GetValue("TEST", () => false);

            Thread.Sleep(TimeSpan.FromSeconds(10));

            value = cacheProvider.GetValue("TEST", () =>
            {
                strategyIsUsed = true;
                return true;
            });

            Assert.That(strategyIsUsed, Is.True);
            Assert.That(value, Is.True);
        }
    }
}