using System;
using System.Linq;
using System.Net;
using Lemonade.Services;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using Lemonade.Web.Infrastructure;
using Lemonade.Web.Mappers;
using Lemonade.Web.Tests.Mocks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Nancy.Testing;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using SelfishHttp;
using Application = Lemonade.Web.Contracts.Application;
using HttpStatusCode = Nancy.HttpStatusCode;

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

            var bootstrapper = new LemonadeBootstrapper();
            bootstrapper.AddDependency(c => c.Register(connectionManager));

            _browser = new Browser(bootstrapper, context => context.UserHostAddress("localhost"));
        }

        [TearDown]
        public void Teardown()
        {
            _server.Dispose();
        }

        [Test]
        public void WhenIPostAFeature_ThenICanGetItViaHttp()
        {
            var application = new Data.Entities.Application { ApplicationId = 1, Name = "TestApplication1" };
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
        public void WhenIPostAFeature_ThenSignalRClientsAreNotified()
        {
            var application = new Data.Entities.Application { ApplicationId = 1, Name = "TestApplication1" };
            _createApplication.Execute(application);

            var feature = GetFeatureModel("MySuperCoolFeature1", _getApplication.Execute(application.Name).ToContract());

            Post(feature);
            _mockClient.Received().addFeature(Arg.Any<dynamic>());
        }

        [Test]
        public void WhenIHaveAnUnknownUrlAppConfigAndITryToResolveAFeatureUsingHttpFeatureResolver_ThenUnknownUrlExceptionIsThrown()
        {
            var application = new Data.Entities.Application { ApplicationId = 1, Name = "TestApplication1" };
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
            var application = new Data.Entities.Application { ApplicationId = 1, Name = "TestApplication1" };
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
            var application = new Data.Entities.Application { ApplicationId = 1, Name = "TestApplication1" };
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

        [Test]
        public void WhenIGetAFeatureWithAHostnameOveride_ThenTheFeatureIsRetrieved()
        {
            var application = new Data.Entities.Application { Name = "TestApplication" };
            new CreateApplication().Execute(application);
            var feature = new Data.Entities.Feature { ApplicationId = application.ApplicationId, Name = "MyTestFeature" };
            new CreateFeature().Execute(feature);

            new CreateFeatureOverride().Execute(new Data.Entities.FeatureOverride { FeatureId = feature.FeatureId, Hostname = Dns.GetHostName(), IsEnabled = true });

            var response = _browser.Get("/api/feature", with =>
            {
                with.Header("Accept", "application/json");
                with.Query("application", application.Name);
                with.Query("feature", feature.Name);
            });

            var result = JsonConvert.DeserializeObject<Contracts.Feature>(response.Body.AsString());
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.FeatureOverrides.Any(f => f.FeatureId == feature.FeatureId && f.IsEnabled), Is.True);
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
                IsEnabled = true,
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