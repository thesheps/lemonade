using System;
using Lemonade.Services;
using Lemonade.Sql.Migrations;
using Lemonade.Web.Infrastructure;
using Nancy.Hosting.Self;
using NUnit.Framework;

namespace Lemonade.AcceptanceTests
{
    public class GivenHttpResourceResolver
    {
        [SetUp]
        public void SetUp()
        {
            Configuration.ResourceResolver = new HttpResourceResolver();
            Runner.SqlCompact("Lemonade").Down();
            Runner.SqlCompact("Lemonade").Up();

            _nancyHost = new NancyHost(new Uri("http://localhost:12345"), new LemonadeBootstrapper());
            _nancyHost.Start();
        }

        [TearDown]
        public void TearDown()
        {
            _nancyHost.Stop();
            _nancyHost.Dispose();
        }

        [Test]
        public void WhenIHaveAKnownLocalisedResourceAndRetrieveIt_ThenTheValueIsCorrect()
        {
        }

        private NancyHost _nancyHost;
    }
}