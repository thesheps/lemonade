using System;
using System.Globalization;
using System.Linq;
using Lemonade.Resolvers;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Mappers;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
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

            _browser = new Browser(new FakeLemonadeBootstrapper());
        }

        [TearDown]
        public void Teardown()
        {
            _server.Dispose();
        }

        [Test]
        public void WhenIPostMultipleFeatures_ThenTheFeaturesAreSavedAndTheSameNumberAreRendered()
        {
            var application = new Core.Entities.Application { Name = "TestApplication1" };
            _saveApplication.Execute(application);
            application = _getApplication.Execute("TestApplication1");

            _browser.Post("/api/feature", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(GetFeatureModel("MySuperCoolFeature1", _getApplication.Execute(application.Name).ToContract())));
            });

            _browser.Post("/api/feature", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(GetFeatureModel("MySuperCoolFeature2", _getApplication.Execute(application.Name).ToContract())));
            });

            var response = _browser.Get("/feature", with =>
            {
                with.Query("applicationId", application.ApplicationId.ToString());
            });

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Body[".feature"].Count(), Is.EqualTo(2));
        }

        [Test]
        public void WhenIHaveMultipleApplications_ThenAllApplicationsAreRendered()
        {
            var save = new SaveApplication();
            save.Execute(new Core.Entities.Application { Name = "TestApplication1" });
            save.Execute(new Core.Entities.Application { Name = "TestApplication2" });
            save.Execute(new Core.Entities.Application { Name = "TestApplication3" });

            var response = _browser.Get("/feature", with =>
            {
                with.Query("application", "TestApplication");
            });

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Body[".application"].Count(), Is.EqualTo(3));
        }

        [Test]
        public void WhenIPostAFeature_ThenICanGetItViaHttp()
        {
            var application = new Core.Entities.Application { ApplicationId = 1, Name = "TestApplication1" };
            _saveApplication.Execute(application);

            _browser.Post("/api/feature", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(GetFeatureModel("MySuperCoolFeature1", _getApplication.Execute(application.Name).ToContract())));
            });

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
        public void WhenIHaveAnUnknownUrlAppConfigAndITryToResolveAFeatureUsingHttpFeatureResolver_ThenUnknownUrlExceptionIsThrown()
        {
            var application = new Core.Entities.Application { ApplicationId = 1, Name = "TestApplication1" };
            _saveApplication.Execute(application);

            _browser.Post("/api/feature", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(GetFeatureModel("MySuperCoolFeature1", _getApplication.Execute(application.Name).ToContract())));
            });

            Assert.Throws<UriFormatException>(() => new HttpFeatureResolver("TestTestTest!!!"));
        }

        [Test]
        public void WhenIPostANewFeature_ThenTheResponseIsRedirectToFeaturesPage()
        {
            var application = new Core.Entities.Application { ApplicationId = 1, Name = "TestApplication1" };
            _saveApplication.Execute(application);

            var postResponse = _browser.Post("/feature/", (with) =>
            {
                with.HttpRequest();
                with.FormValue("applicationId", "1");
                with.FormValue("name", "Feature1");
                with.FormValue("startDate", DateTime.Now.ToShortDateString());
                with.FormValue("expirationDays", "10");
                with.FormValue("isEnabled", "true");
            });

            Assert.That(postResponse.StatusCode, Is.EqualTo(HttpStatusCode.SeeOther));
            Assert.That(postResponse.Headers["Location"], Is.EqualTo("/feature?applicationId=1"));
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
                ApplicationId = application.Id
            };
        }

        private Browser _browser;
        private Server _server;
        private SaveApplication _saveApplication;
        private GetApplicationByName _getApplication;
        private const string ConnectionString = "Lemonade";
    }
}