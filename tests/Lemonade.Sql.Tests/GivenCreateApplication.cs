using Lemonade.Builders;
using Lemonade.Data.Exceptions;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using NUnit.Framework;

namespace Lemonade.Sql.Tests
{
    public class GivenCreateApplication
    {
        [SetUp]
        public void SetUp()
        {
            Runner.SqlCompact("Lemonade").Down();
            Runner.SqlCompact("Lemonade").Up();
        }

        [Test]
        public void WhenISaveAnApplication_ThenICanRetrieveIt()
        {
            var saveApplication = new CreateApplication();
            var getApplicationByName = new GetApplicationByName();
            saveApplication.Execute(new ApplicationBuilder()
                .WithName("Test12345")
                .Build());

            var application = getApplicationByName.Execute("Test12345");
            Assert.That(application, Is.Not.Null);
            Assert.That(application.Name, Is.EqualTo("Test12345"));
        }

        [Test]
        public void WhenITryToSaveADuplicateApplication_ThenSaveApplicationExceptionIsThrown()
        {
            var saveApplication = new CreateApplication();
            var application = new ApplicationBuilder()
                .WithName("Test12345")
                .Build();

            saveApplication.Execute(application);

            Assert.Throws<CreateApplicationException>(() => saveApplication.Execute(application));
        }
    }
}