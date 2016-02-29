using System;
using System.Globalization;
using System.Web;
using Lemonade.Builders;
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

            var application = new ApplicationBuilder()
                .WithName("Test Application")
                .Build();

            new CreateApplication().Execute(application);

            var resource = new ResourceBuilder()
                .WithLocale("de-DE")
                .WithResourceKey("HelloWorld")
                .WithResourceSet("MyTestResources")
                .WithValue("Hello World")
                .WithApplication(application).Build();

            new CreateResource().Execute(resource);

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
            var culture = CultureInfo.CreateSpecificCulture("de-DE");
            var test = HttpContext.GetGlobalResourceObject("MyTestResources", "HelloWorld", culture);
            Assert.That(test, Is.EqualTo("Hello World"));
        }

        private NancyHost _nancyHost;
    }
}