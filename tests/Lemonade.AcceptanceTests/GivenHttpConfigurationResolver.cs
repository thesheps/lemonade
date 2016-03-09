using System;
using Lemonade.Builders;
using Lemonade.Data.Entities;
using Lemonade.Fakes;
using Lemonade.Services;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using Lemonade.Web.Infrastructure;
using Nancy.Hosting.Self;
using NUnit.Framework;

namespace Lemonade.AcceptanceTests
{
    public class GivenHttpConfigurationResolver
    {
        [SetUp]
        public void SetUp()
        {
            var application = new ApplicationBuilder().WithName("Test Application").Build();

            Configuration.ConfigurationResolver = new HttpConfigurationResolver("http://localhost:12345");
            Runner.SqlCompact("Lemonade").Down();
            Runner.SqlCompact("Lemonade").Up();

            new CreateApplicationFake().Execute(application);
            _application = new GetApplicationByName().Execute(application.Name);

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
        public void WhenIHaveAKnownStringConfigurationValueAndITryToRetrieveIt_ThenTheValueIsAsExpected()
        {
            var configuration = new ConfigurationBuilder().WithName("MyTestString").WithApplication(_application).WithValue("Hello World").Build();
            new CreateConfigurationFake().Execute(configuration);

            var value = Config.Settings<string>("MyTestString");
            Assert.That(value, Is.EqualTo("Hello World"));
        }

        [Test]
        public void WhenIHaveAKnownBooleanConfigurationValueAndITryToRetrieveIt_ThenTheValueIsAsExpected()
        {
            var configuration = new ConfigurationBuilder().WithName("MyTestBoolean").WithApplication(_application).WithValue("true").Build();
            new CreateConfigurationFake().Execute(configuration);

            var value = Config.Settings<bool>("MyTestBoolean");
            Assert.That(value, Is.True);
        }

        [Test]
        public void WhenIHaveAKnownIntegerConfigurationValueAndITryToRetrieveIt_ThenTheValueIsAsExpected()
        {
            var configuration = new ConfigurationBuilder().WithName("MyTestInteger").WithApplication(_application).WithValue("1").Build();
            new CreateConfigurationFake().Execute(configuration);

            var value = Config.Settings<int>("MyTestInteger");
            Assert.That(value, Is.EqualTo(1));
        }

        [Test]
        public void WhenIHaveAKnownDoubleConfigurationValueAndITryToRetrieveIt_ThenTheValueIsAsExpected()
        {
            var configuration = new ConfigurationBuilder().WithName("MyTestDouble").WithApplication(_application).WithValue("3.142").Build();
            new CreateConfigurationFake().Execute(configuration);

            var value = Config.Settings<double>("MyTestDouble");
            Assert.That(value, Is.EqualTo(3.142));
        }

        [Test]
        public void WhenIHaveAKnownDecimalConfigurationValueAndITryToRetrieveIt_ThenTheValueIsAsExpected()
        {
            var configuration = new ConfigurationBuilder().WithName("MyTestDecimal").WithApplication(_application).WithValue("10.57").Build();
            new CreateConfigurationFake().Execute(configuration);

            var value = Config.Settings<decimal>("MyTestDecimal");
            Assert.That(value, Is.EqualTo(10.57m));
        }

        [Test]
        public void WhenIHaveAKnownDateTimeConfigurationValueAndITryToRetrieveIt_ThenTheValueIsAsExpected()
        {
            var dateTime = DateTime.Now;
            var configuration = new ConfigurationBuilder().WithName("MyTestDateTime").WithApplication(_application).WithValue(dateTime.ToString()).Build();
            new CreateConfigurationFake().Execute(configuration);

            var value = Config.Settings<DateTime>("MyTestDateTime");
            Assert.That(value, Is.EqualTo(DateTime.Parse(dateTime.ToString())));
        }

        [Test]
        public void WhenIHaveAKnownUriConfigurationValueAndITryToRetrieveIt_ThenTheValueIsAsExpected()
        {
            const string uri = "http://localhost:51346/";
            var configuration = new ConfigurationBuilder().WithName("MyTestUri").WithApplication(_application).WithValue(uri).Build();
            new CreateConfigurationFake().Execute(configuration);

            var value = Config.Settings<Uri>("MyTestUri");
            Assert.That(value.AbsoluteUri, Is.EqualTo(uri));
        }

        private NancyHost _nancyHost;
        private Application _application;
    }
}