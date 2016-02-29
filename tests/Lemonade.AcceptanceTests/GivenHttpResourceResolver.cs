using System;
using System.Web;
using Lemonade.Data.Entities;
using Lemonade.Services;
using Lemonade.Sql.Commands;
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

            var application = new Application { Name = "Hello World" };
            new CreateApplication().Execute(application);

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
            var test = HttpContext.GetGlobalResourceObject("Hello", "World");

        }

        private NancyHost _nancyHost;
    }
}