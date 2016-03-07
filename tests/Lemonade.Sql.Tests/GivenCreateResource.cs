using Lemonade.Builders;
using Lemonade.Data.Entities;
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

            var locale = new Locale() { Description = "English", IsoCode = "en-GB" };

            new CreateApplicationFake().Execute(application);
            new CreateLocaleFake().Execute(locale);

            var resource = new ResourceBuilder()
                .WithLocale(locale)
                .WithResourceKey("HelloWorld")
                .WithResourceSet("MyTestResources")
                .WithValue("Hello World")
                .WithApplication(application).Build();

            new CreateResourceFake().Execute(resource);

            var resources = new GetAllResourcesByApplicationId().Execute(application.ApplicationId);

            Assert.That(resources[0].Locale.IsoCode, Is.EqualTo("en-GB"));
            Assert.That(resources[0].ResourceKey, Is.EqualTo("HelloWorld"));
            Assert.That(resources[0].ResourceSet, Is.EqualTo("MyTestResources"));
            Assert.That(resources[0].Value, Is.EqualTo("Hello World"));
        }
    }
}