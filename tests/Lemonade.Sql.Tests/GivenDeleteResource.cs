using Lemonade.Builders;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using NUnit.Framework;

namespace Lemonade.Sql.Tests
{
    public class GivenDeleteResource
    {
        [SetUp]
        public void SetUp()
        {
            _createResource = new CreateResource();
            _deleteFeature = new DeleteResource();
            _getResource = new GetResource();
            Runner.SqlCompact("Lemonade").Down();
            Runner.SqlCompact("Lemonade").Up();
        }

        [Test]
        public void WhenIDeleteAResource_ThenItIsNoLongerAvailable()
        {
            var application = new ApplicationBuilder()
                .WithName("Test12345")
                .Build();

            new CreateApplication().Execute(application);

            var resource = new ResourceBuilder()
                .WithLocale("de-DE")
                .WithResourceKey("HelloWorld")
                .WithResourceSet("MyTestResources")
                .WithValue("Hello World")
                .WithApplication(application).Build();

            _createResource.Execute(resource);
            _deleteFeature.Execute(resource.ResourceId);
            resource = _getResource.Execute(application.Name, resource.ResourceSet, resource.ResourceKey, resource.Locale);

            Assert.That(resource, Is.Null);
        }

        private CreateResource _createResource;
        private GetResource _getResource;
        private DeleteResource _deleteFeature;
    }
}