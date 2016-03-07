using System;
using System.Globalization;
using System.Web;
using Lemonade.Builders;
using Lemonade.Data.Commands;
using Lemonade.Data.Entities;
using Lemonade.Fakes;
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

            var locale = new Locale { Description = "English", IsoCode = "en-GB" };

            new CreateApplicationFake().Execute(application);
            new CreateLocaleFake().Execute(locale);

            var resource = new ResourceBuilder()
                .WithLocale(locale)
                .WithResourceKey("HelloWorld")
                .WithResourceSet("MyTestResources")
                .WithValue("Hello World")
                .WithApplication(application).Build();

            new CreateResourceFake().Execute(resource);

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
            var culture = CultureInfo.CreateSpecificCulture("en-GB");
            var test = HttpContext.GetGlobalResourceObject("MyTestResources", "HelloWorld", culture);
            Assert.That(test, Is.EqualTo("Hello World"));
        }

        private NancyHost _nancyHost;
    }
}