using Lemonade.AcceptanceTests;
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
using Application = Lemonade.Data.Entities.Application;

namespace Lemonade.Web.Tests
{
    public class GivenFeatureOverridesModule
    {
        [SetUp]
        public void SetUp()
        {
            _getFeature = new GetFeatureByNameAndApplication();
            _createApplication = new CreateApplication();
            _createFeature = new CreateFeature();
            _createFeatureOverride = new CreateFeatureOverride();
            _server = new Server(64978);

            Runner.SqlCompact(ConnectionString).Down();
            Runner.SqlCompact(ConnectionString).Up();

            var hubContext = Substitute.For<IHubContext>();
            var connectionManager = Substitute.For<IConnectionManager>();
            connectionManager.GetHubContext<LemonadeHub>().Returns(hubContext);

            _mockClient = Substitute.For<IMockClient>();
            SubstituteExtensions.Returns(hubContext.Clients.All, _mockClient);

            var bootstrapper = new TestLemonadeBootstrapper();
            bootstrapper.AddDependency(c => c.Register(connectionManager));

            _browser = new Browser(bootstrapper, context => context.UserHostAddress("localhost"));
        }

        [TearDown]
        public void Teardown()
        {
            _server.Dispose();
        }

        [Test]
        public void WhenIPostAFeatureOverride_ThenItIsSaved()
        {
            var application = new Application { ApplicationId = 1, Name = "TestApplication1" };
            _createApplication.Execute(application);

            var feature = new Data.Entities.Feature { ApplicationId = application.ApplicationId, Name = "Feature1" };
            _createFeature.Execute(feature);

            var featureOverride = new Data.Entities.FeatureOverride { FeatureId = feature.FeatureId, Hostname = "Test", IsEnabled = true };
            _createFeatureOverride.Execute(featureOverride);

            Post(featureOverride.ToContract());

            var response = _browser.Get("/api/feature", with =>
            {
                with.Header("Accept", "application/json");
                with.Query("application", application.Name);
                with.Query("feature", feature.Name);
            });

            var result = JsonConvert.DeserializeObject<Contracts.Feature>(response.Body.AsString());
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.FeatureOverrides[0].IsEnabled, Is.True);
        }

        [Test]
        public void WhenIPostAFeatureOverride_ThenSignalRClientsAreNotified()
        {
            var application = new Application { ApplicationId = 1, Name = "TestApplication1" };
            _createApplication.Execute(application);

            var feature = new Data.Entities.Feature { ApplicationId = application.ApplicationId, Name = "Feature1" };
            _createFeature.Execute(feature);

            var featureOverride = new Data.Entities.FeatureOverride { FeatureId = feature.FeatureId, Hostname = "Test", IsEnabled = true };

            Post(featureOverride.ToContract());
            _mockClient.Received().addFeatureOverride(Arg.Any<dynamic>());
        }

        [Test]
        public void WhenIDeleteAFeatureOverride_ThenTheFeatureOverrideIsRemoved()
        {
            var application = new Application { ApplicationId = 1, Name = "TestApplication1" };
            _createApplication.Execute(application);

            var feature = new Data.Entities.Feature { ApplicationId = application.ApplicationId, Name = "Feature1" };
            _createFeature.Execute(feature);

            var featureOverride = new Data.Entities.FeatureOverride { FeatureId = feature.FeatureId, Hostname = "Test", IsEnabled = true };
            _createFeatureOverride.Execute(featureOverride);

            _browser.Delete("/api/featureoverrides", with => { with.Query("id", "1"); });

            feature = _getFeature.Execute(feature.Name, application.Name);
            CollectionAssert.IsEmpty(feature.FeatureOverrides);
        }

        [Test]
        public void WhenIPutAFeature_ThenTheFeatureOverrideIsUpdated()
        {
            var application = new Application { ApplicationId = 1, Name = "TestApplication1" };
            _createApplication.Execute(application);

            var feature = new Data.Entities.Feature { ApplicationId = application.ApplicationId, Name = "Feature1" };
            _createFeature.Execute(feature);

            var featureOverride = new Data.Entities.FeatureOverride { FeatureId = feature.FeatureId, Hostname = "Test", IsEnabled = true };
            _createFeatureOverride.Execute(featureOverride);

            featureOverride.Hostname = "TEST123";

            Put(featureOverride.ToContract());

            feature = _getFeature.Execute(feature.Name, application.Name);
            Assert.That(feature.FeatureOverrides[0].Hostname, Is.EqualTo("TEST123"));
        }

        private void Post(FeatureOverride featureOverride)
        {
            _browser.Post("/api/featureoverrides", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(featureOverride));
            });
        }

        private void Put(FeatureOverride featureOverride)
        {
            _browser.Put("/api/featureoverrides", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(featureOverride));
            });
        }

        private Browser _browser;
        private Server _server;
        private GetFeatureByNameAndApplication _getFeature;
        private CreateApplication _createApplication;
        private CreateFeature _createFeature;
        private CreateFeatureOverride _createFeatureOverride;
        private IMockClient _mockClient;
        private const string ConnectionString = "Lemonade";
    }
}