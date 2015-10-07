using System;
using Lemonade.Resolvers;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Infrastructure;
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
            _createApplication = new CreateApplication();
            _getApplication = new GetApplicationByName();
            _getFeature = new GetFeatureByNameAndApplication();
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

        [Test]
        public void WhenIPostAFeature_ThenICanGetItViaHttp()
        {
            var application = new Core.Domain.Application { ApplicationId = 1, Name = "TestApplication1" };
            _createApplication.Execute(application);

            var feature = GetFeatureModel("MySuperCoolFeature1", _getApplication.Execute(application.Name).ToContract());

            Post(feature);

            var response = _browser.Get("/api/feature", with =>
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
        public void WhenIPostAFeature_SignalRClientsAreNotified()
        {
            var application = new Core.Domain.Application { ApplicationId = 1, Name = "TestApplication1" };
            _createApplication.Execute(application);

            var feature = GetFeatureModel("MySuperCoolFeature1", _getApplication.Execute(application.Name).ToContract());

            Post(feature);
            _mockClient.Received().addFeature(Arg.Any<dynamic>());
        }

        [Test]
        public void WhenIHaveAnUnknownUrlAppConfigAndITryToResolveAFeatureUsingHttpFeatureResolver_ThenUnknownUrlExceptionIsThrown()
        {
            var application = new Core.Domain.Application { ApplicationId = 1, Name = "TestApplication1" };
            _createApplication.Execute(application);

            _browser.Post("/api/features", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(GetFeatureModel("MySuperCoolFeature1", _getApplication.Execute(application.Name).ToContract())));
            });

            Assert.Throws<UriFormatException>(() => new HttpFeatureResolver("TestTestTest!!!"));
        }

        [Test]
        public void WhenIDeleteAFeature_ThenTheFeatureIsRemoved()
        {
            var application = new Core.Domain.Application { ApplicationId = 1, Name = "TestApplication1" };
            _createApplication.Execute(application);

            var featureModel = GetFeatureModel("MySuperCoolFeature1", _getApplication.Execute(application.Name).ToContract());
            Post(featureModel);

            _browser.Delete("/api/features", with => { with.Query("id", "1"); });

            var feature = _getFeature.Execute(featureModel.Name, application.Name);
            Assert.That(feature, Is.Null);
        }

        [Test]
        public void WhenIPutAFeature_ThenTheFeatureIsUpdated()
        {
            var application = new Core.Domain.Application { ApplicationId = 1, Name = "TestApplication1" };
            _createApplication.Execute(application);

            var featureModel = GetFeatureModel("MySuperCoolFeature1", _getApplication.Execute(application.Name).ToContract());
            Post(featureModel);

            var feature = _getFeature.Execute("MySuperCoolFeature1", application.Name);
            featureModel = feature.ToContract();
            featureModel.Name = "Ponies";

            Put(featureModel);

            feature = _getFeature.Execute("MySuperCoolFeature1", application.Name);
            Assert.That(feature, Is.Null);
        }

        private void Post(Contracts.Feature feature)
        {
            _browser.Post("/api/features", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(feature));
            });
        }

        private void Put(Contracts.Feature feature)
        {
            _browser.Put("/api/features", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(feature));
            });
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
        private CreateApplication _createApplication;
        private GetApplicationByName _getApplication;
        private GetFeatureByNameAndApplication _getFeature;
        private IMockClient _mockClient;
        private const string ConnectionString = "Lemonade";
    }
}