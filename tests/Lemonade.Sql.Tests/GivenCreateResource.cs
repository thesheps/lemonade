using Lemonade.Builders;
using Lemonade.Fakes;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using NUnit.Framework;

namespace Lemonade.Sql.Tests
{
    public class GivenCreateResource
    {
        [SetUp]
        public void SetUp()
        {
            Runner.SqlCompact("Lemonade").Down();
            Runner.SqlCompact("Lemonade").Up();
        }

        [Test]
        public void WhenITryToSaveAResource_ThenTheResourceIsSaved()
        {
            var application = new ApplicationBuilder()
                .WithName("Test12345")
                .Build();

            new CreateApplicationFake().Execute(application);

            var resource = new ResourceBuilder()
                .WithLocale("de-DE")
                .WithResourceKey("HelloWorld")
                .WithResourceSet("MyTestResources")
                .WithValue("Hello World")
                .WithApplication(application).Build();

            new CreateResourceFake().Execute(resource);

            var resources = new GetAllResourcesByApplicationId().Execute(application.ApplicationId);

            Assert.That(resources[0].Locale, Is.EqualTo("de-DE"));
            Assert.That(resources[0].ResourceKey, Is.EqualTo("HelloWorld"));
            Assert.That(resources[0].ResourceSet, Is.EqualTo("MyTestResources"));
            Assert.That(resources[0].Value, Is.EqualTo("Hello World"));
        }
    }
}