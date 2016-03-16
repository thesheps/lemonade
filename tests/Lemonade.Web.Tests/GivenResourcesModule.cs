using Lemonade.Fakes;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Core.Mappers;
using Lemonade.Web.Tests.Mocks;
using Nancy.Testing;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using SelfishHttp;
using Application = Lemonade.Web.Contracts.Application;
using HttpStatusCode = Nancy.HttpStatusCode;

namespace Lemonade.Web.Tests
{
    public class GivenResourcesModule
    {
        [SetUp]
        public void SetUp()
        {
            _createApplication = new CreateApplicationFake();
            _getApplication = new GetApplicationByName();
            _getResource = new GetResource();
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
        public void WhenIPostAResource_ThenICanGetItViaHttpAndSignalRClientsAreNotified()
        {
            var application = new Data.Entities.Application { ApplicationId = 1, Name = "TestApplication1" };
            _createApplication.Execute(application);

            var locale = new GetAllLocales().Execute()[10].ToContract();
            var resource = GetResourceModel(locale, "Test", "Test", "Test", application.ToContract());

            Post(resource);

            var response = _browser.Get("/api/resource", with =>
            {
                with.Header("Accept", "application/json");
                with.Query("application", application.Name);
                with.Query("resourceSet", resource.ResourceSet);
                with.Query("resourceKey", resource.ResourceKey);
                with.Query("locale", resource.Locale.IsoCode);
            });

            var result = JsonConvert.DeserializeObject<Resource>(response.Body.AsString());
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.ApplicationId, Is.EqualTo(application.ApplicationId));
            Assert.That(result.ResourceKey, Is.EqualTo(resource.ResourceKey));
            Assert.That(result.ResourceSet, Is.EqualTo(resource.ResourceSet));
            Assert.That(result.Locale.IsoCode, Is.EqualTo(resource.Locale.IsoCode));

            _bootstrapper
                .Resolve<IMockClient>()
                .Received()
                .addResource(Arg.Any<dynamic>());
        }

        [Test]
        public void WhenIDeleteAResource_ThenTheResourceIsRemovedAndSignalRClientsAreNotified()
        {
            var application = new Data.Entities.Application { ApplicationId = 1, Name = "TestApplication1" };
            _createApplication.Execute(application);

            var locale = new GetAllLocales().Execute()[0].ToContract();
            var resourceModel = GetResourceModel(locale, "Test", "Test", "Test", _getApplication.Execute(application.Name).ToContract());
            Post(resourceModel);

            _browser.Delete("/api/resources", with => { with.Query("id", "1"); });

            var resource = _getResource.Execute(application.Name, "Test", "Test", "Test");
            Assert.That(resource, Is.Null);

            _bootstrapper
                .Resolve<IMockClient>()
                .Received()
                .removeResource(Arg.Any<dynamic>());
        }

        [Test]
        public void WhenIPutAResource_ThenTheResourceIsUpdatedAndSignalRClientsAreNotified()
        {
            var application = new Data.Entities.Application { ApplicationId = 1, Name = "TestApplication1" };
            _createApplication.Execute(application);

            var locale = new GetAllLocales().Execute()[0].ToContract();
            var resourceModel = GetResourceModel(locale, "Test", "Test", "Test", application.ToContract());
            Post(resourceModel);

            var resource = _getResource.Execute(application.Name, "Test", "Test", locale.IsoCode);
            resourceModel = resource.ToContract();
            resourceModel.Value = "Ponies";

            Put(resourceModel);

            resource = _getResource.Execute(application.Name, "Test", "Test", locale.IsoCode);
            Assert.That(resource.Value, Is.EqualTo("Ponies"));

            _bootstrapper
                .Resolve<IMockClient>()
                .Received()
                .updateResource(Arg.Any<dynamic>());
        }

        [Test]
        public void WhenIGenerateResourcesForAGivenLocale_ThenICanGetItViaHttpAndSignalRClientsAreNotified()
        {
            var application = new Data.Entities.Application { ApplicationId = 1, Name = "TestApplication1" };
            _createApplication.Execute(application);

            var locale = new GetAllLocales().Execute()[10].ToContract();
            var targetLocale = new GetAllLocales().Execute()[1].ToContract();
            var resource = GetResourceModel(locale, "Test", "Test", "Test", application.ToContract());
            var generateResourcesModel = GetGenerateResourcesModel(application.ApplicationId, locale.LocaleId, targetLocale.LocaleId, "pseudo");

            Post(resource);
            Post(generateResourcesModel);

            var response = _browser.Get("/api/resource", with =>
            {
                with.Header("Accept", "application/json");
                with.Query("application", application.Name);
                with.Query("resourceSet", resource.ResourceSet);
                with.Query("resourceKey", resource.ResourceKey);
                with.Query("locale", targetLocale.IsoCode);
            });

            var result = JsonConvert.DeserializeObject<Resource>(response.Body.AsString());
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.ApplicationId, Is.EqualTo(application.ApplicationId));
            Assert.That(result.ResourceKey, Is.EqualTo(resource.ResourceKey));
            Assert.That(result.ResourceSet, Is.EqualTo(resource.ResourceSet));
            Assert.That(result.Locale.IsoCode, Is.EqualTo(resource.Locale.IsoCode));

            _bootstrapper
                .Resolve<IMockClient>()
                .Received()
                .addResource(Arg.Any<dynamic>());
        }

        private void Post(Resource resource)
        {
            _browser.Post("/api/resources", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(resource));
            });
        }

        private void Post(GenerateResources contract)
        {
            _browser.Post("/api/resources/generate", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(contract));
            });
        }

        private void Put(Resource resource)
        {
            _browser.Put("/api/resources", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(resource));
            });
        }

        private static Resource GetResourceModel(Locale locale, string resourceKey, string resourceSet, string value, Application application)
        {
            return new Resource
            {
                LocaleId = locale.LocaleId,
                Locale = locale,
                ResourceKey = resourceKey,
                ResourceSet = resourceSet,
                Value = value,
                Application = application,
                ApplicationId = application.ApplicationId
            };
        }

        private static GenerateResources GetGenerateResourcesModel(int applicationId, int localeId, int targetLocaleId, string type)
        {
            return new GenerateResources { ApplicationId = applicationId, LocaleId = localeId, TargetLocaleId = targetLocaleId, Type = type };
        }

        private Browser _browser;
        private Server _server;
        private CreateApplicationFake _createApplication;
        private GetApplicationByName _getApplication;
        private GetResource _getResource;
        private TestBootstrapper _bootstrapper;
        private const string ConnectionString = "Lemonade";
    }
}