using System.Collections.Generic;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using Lemonade.Web.Contracts;
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
    public class GivenResourcesModule
    {
        [SetUp]
        public void SetUp()
        {
            _createApplication = new CreateApplication();
            _getApplication = new GetApplicationByName();
            _getResource = new GetResource();
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
        public void WhenIPostAResource_ThenICanGetItViaHttp()
        {
            var application = new Data.Entities.Application { ApplicationId = 1, Name = "TestApplication1" };
            _createApplication.Execute(application);

            var resource = GetResourceModel("de-DE", "Test", "Test", "Test", application.ToContract());

            Post(resource);

            var response = _browser.Get("/api/resource", with =>
            {
                with.Header("Accept", "application/json");
                with.Query("application", application.Name);
                with.Query("resourceSet", resource.ResourceSet);
                with.Query("resourceKey", resource.ResourceKey);
                with.Query("locale", resource.Locale);
            });

            var result = JsonConvert.DeserializeObject<Resource>(response.Body.AsString());
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.ApplicationId, Is.EqualTo(application.ApplicationId));
            Assert.That(result.ResourceKey, Is.EqualTo(resource.ResourceKey));
            Assert.That(result.ResourceSet, Is.EqualTo(resource.ResourceSet));
            Assert.That(result.Locale, Is.EqualTo(resource.Locale));
        }

        [Test]
        public void WhenIPostMultipleResources_ThenICanGetItViaHttpAndSignalRClientsAreNotified()
        {
            var application = new Data.Entities.Application { ApplicationId = 1, Name = "TestApplication1" };
            _createApplication.Execute(application);

            Post(GetResourceModel("de-DE", "Test", "Test1", "Test1", application.ToContract()));
            Post(GetResourceModel("de-DE", "Test", "Test2", "Test2", application.ToContract()));
            Post(GetResourceModel("de-DE", "Test", "Test3", "Test3", application.ToContract()));

            var response = _browser.Get("/api/resources", with =>
            {
                with.Header("Accept", "application/json");
                with.Query("applicationId", application.ApplicationId.ToString());
            });

            var result = JsonConvert.DeserializeObject<IList<Resource>>(response.Body.AsString());
            Assert.That(result.Count, Is.EqualTo(3));
            _mockClient.Received().addResource(Arg.Any<dynamic>());
        }

        [Test]
        public void WhenIDeleteAResource_ThenTheResourceIsRemovedAndSignalRClientsAreNotified()
        {
            var application = new Data.Entities.Application { ApplicationId = 1, Name = "TestApplication1" };
            _createApplication.Execute(application);

            var resourceModel = GetResourceModel("Test", "Test", "Test", "Test", _getApplication.Execute(application.Name).ToContract());
            Post(resourceModel);

            _browser.Delete("/api/resources", with => { with.Query("id", "1"); });

            var resource = _getResource.Execute(application.Name, "Test", "Test", "Test");
            Assert.That(resource, Is.Null);
            _mockClient.Received().removeResource(Arg.Any<dynamic>());
        }

        [Test]
        public void WhenIPutAResource_ThenTheResourceIsUpdatedAndSignalRClientsAreNotified()
        {
            var application = new Data.Entities.Application { ApplicationId = 1, Name = "TestApplication1" };
            _createApplication.Execute(application);

            var resourceModel = GetResourceModel("Test", "Test", "Test", "Test", application.ToContract());
            Post(resourceModel);

            var resource = _getResource.Execute(application.Name, "Test", "Test", "test");
            resourceModel = resource.ToContract();
            resourceModel.Value = "Ponies";

            Put(resourceModel);

            resource = _getResource.Execute(application.Name, "Test", "Test", "Test");
            Assert.That(resource.Value, Is.EqualTo("Ponies"));
            _mockClient.Received().updateResource(Arg.Any<dynamic>());
        }

        private void Post(Resource resource)
        {
            _browser.Post("/api/resources", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(resource));
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

        private static Resource GetResourceModel(string locale, string resourceKey, string resourceSet, string value, Application application)
        {
            return new Resource
            {
                Locale = locale,
                ResourceKey = resourceKey,
                ResourceSet = resourceSet,
                Value = value,
                Application = application,
                ApplicationId = application.ApplicationId
            };
        }

        private Browser _browser;
        private Server _server;
        private CreateApplication _createApplication;
        private GetApplicationByName _getApplication;
        private GetResource _getResource;
        private IMockClient _mockClient;
        private const string ConnectionString = "Lemonade";
    }
}