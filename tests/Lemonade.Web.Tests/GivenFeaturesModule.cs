using System;
using System.Linq;
using Lemonade.Resolvers;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Mappers;
using Lemonade.Web.Tests.Mocks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using SelfishHttp;

namespace Lemonade.Web.Tests
{
    public class GivenFeaturesModule
    {
        [SetUp]
        public void SetUp()
        {
            _saveApplication = new SaveApplication();
            _getApplication = new GetApplicationByName();
            _server = new Server(64978);
            Runner.SqlCompact(ConnectionString).Down();
            Runner.SqlCompact(ConnectionString).Up();

            var hubContext = Substitute.For<IHubContext>();
            var connectionManager = Substitute.For<IConnectionManager>();
            connectionManager.GetHubContext<LemonadeHub>().Returns(hubContext);

            _mockClient = Substitute.For<IMockClient>();
            SubstituteExtensions.Returns(hubContext.Clients.All, _mockClient);

            var bootstrapper = new TestLemonadeBootstrapper();
            bootstrapper.ConfigureDependency(c => c.Register(connectionManager));

            _browser = new Browser(bootstrapper);
        }

        [TearDown]
        public void Teardown()
        {
            _server.Dispose();
        }

        public void WhenIHaveMultipleApplications_ThenAllApplicationsAreRendered()
        {
            var save = new SaveApplication();
            save.Execute(new Core.Domain.Application { Name = "TestApplication1" });
            save.Execute(new Core.Domain.Application { Name = "TestApplication2" });
            save.Execute(new Core.Domain.Application { Name = "TestApplication3" });

            var response = _browser.Get("/features", with =>
            {
                with.Query("application", "TestApplication");
            });

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Body[".application"].Count(), Is.EqualTo(3));
        }

        [Test]
        public void WhenIPostAFeature_ThenICanGetItViaHttp()
        {
            var application = new Core.Domain.Application { ApplicationId = 1, Name = "TestApplication1" };
            _saveApplication.Execute(application);

            _browser.Post("/api/features", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(GetFeatureModel("MySuperCoolFeature1", _getApplication.Execute(application.Name).ToContract())));
            });

            var response = _browser.Get("/api/features", with =>
            {
                with.Header("Accept", "application/json");
                with.Query("application", application.Name);
                with.Query("feature", "MySuperCoolFeature1");
            });

            var result = JsonConvert.DeserializeObject<Contracts.Feature>(response.Body.AsString());
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.IsEnabled, Is.True);
        }

        [Test]
        public void WhenIHaveAnUnknownUrlAppConfigAndITryToResolveAFeatureUsingHttpFeatureResolver_ThenUnknownUrlExceptionIsThrown()
        {
            var application = new Core.Domain.Application { ApplicationId = 1, Name = "TestApplication1" };
            _saveApplication.Execute(application);

            _browser.Post("/api/features", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(GetFeatureModel("MySuperCoolFeature1", _getApplication.Execute(application.Name).ToContract())));
            });

            Assert.Throws<UriFormatException>(() => new HttpFeatureResolver("TestTestTest!!!"));
        }

        private static Contracts.Feature GetFeatureModel(string name, Application application)
        {
            return new Contracts.Feature
            {
                ExpirationDays = 1,
                IsEnabled = true,
                StartDate = DateTime.Now,
                Name = name,
                Application = application,
                ApplicationId = application.ApplicationId
            };
        }

        private Browser _browser;
        private Server _server;
        private SaveApplication _saveApplication;
        private GetApplicationByName _getApplication;
        private IMockClient _mockClient;
        private const string ConnectionString = "Lemonade";
    }
}