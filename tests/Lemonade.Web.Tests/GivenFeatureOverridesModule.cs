using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Mappers;
using Lemonade.Web.Tests.Mocks;
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

            _bootstrapper = new TestBootstrapper();
            _browser = new Browser(_bootstrapper, context => context.UserHostAddress("localhost"));
        }

        [TearDown]
        public void Teardown()
        {
            _server.Dispose();
        }

        [Test]
        public void WhenIPostAFeatureOverride_ThenItIsSavedAndSignalRClientsAreNotified()
        {
            var application = new Application { ApplicationId = 1, Name = "TestApplication1" };
            _createApplication.Execute(application);

            var feature = new Data.Entities.Feature { ApplicationId = application.ApplicationId, Name = "Feature1" };
            _createFeature.Execute(feature);

            var featureOverride = new FeatureOverride { FeatureId = feature.FeatureId, Hostname = "Test", IsEnabled = true };

            Post(featureOverride);

            var response = _browser.Get("/api/feature", with =>
            {
                with.Header("Accept", "application/json");
                with.Query("application", application.Name);
                with.Query("feature", feature.Name);
            });

            var result = JsonConvert.DeserializeObject<Contracts.Feature>(response.Body.AsString());
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.FeatureOverrides[0].IsEnabled, Is.True);

            _bootstrapper
                .Resolve<IMockClient>()
                .Received()
                .addFeatureOverride(Arg.Any<dynamic>());
        }

        [Test]
        public void WhenIDeleteAFeatureOverride_ThenTheFeatureOverrideIsRemovedAndSignalRClientsAreNotified()
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

            _bootstrapper
                .Resolve<IMockClient>()
                .Received()
                .removeFeatureOverride(Arg.Any<dynamic>());
        }

        [Test]
        public void WhenIPutAFeature_ThenTheFeatureOverrideIsUpdatedAndSignalRClientsAreNotified()
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

            _bootstrapper
                .Resolve<IMockClient>()
                .Received()
                .updateFeatureOverride(Arg.Any<dynamic>());
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
        private TestBootstrapper _bootstrapper;
        private const string ConnectionString = "Lemonade";
    }
}