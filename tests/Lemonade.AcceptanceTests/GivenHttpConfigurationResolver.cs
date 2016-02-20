using System;
using System.Net;
using Lemonade.Builders;
using Lemonade.Services;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
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
            _createApplication = new CreateApplication();
            _getApplicationByName = new GetApplicationByName();
            _createApplication.Execute(application);
            application = _getApplicationByName.Execute(application.Name);

            _nancyHost = new NancyHost(new Uri("http://localhost:12345"), new TestLemonadeBootstrapper());
            _nancyHost.Start();
        }

        [TearDown]
        public void TearDown()
        {
            _nancyHost.Stop();
            _nancyHost.Dispose();
        }

        private NancyHost _nancyHost;
        private CreateApplication _createApplication;
        private GetApplicationByName _getApplicationByName;
    }
}