using Lemonade.Builders;
using Lemonade.Data.Entities;
using Lemonade.Fakes;
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
            _createResource = new CreateResourceFake();
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

            var locale = new Locale { Description = "English", IsoCode = "en-GB" };

            new CreateApplicationFake().Execute(application);
            new CreateLocaleFake().Execute(locale);

            var resource = new ResourceBuilder()
                .WithLocale(locale)
                .WithResourceKey("HelloWorld")
                .WithResourceSet("MyTestResources")
                .WithValue("Hello World")
                .WithApplication(application).Build();

            _createResource.Execute(resource);
            _deleteFeature.Execute(resource.ResourceId);
            resource = _getResource.Execute(application.Name, resource.ResourceSet, resource.ResourceKey, resource.Locale.IsoCode);

            Assert.That(resource, Is.Null);
        }

        private CreateResourceFake _createResource;
        private GetResource _getResource;
        private DeleteResource _deleteFeature;
    }
}