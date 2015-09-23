using Lemonade.Builders;
using Lemonade.Data.Exceptions;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using NUnit.Framework;

namespace Lemonade.Sql.Tests
{
    public class GivenSaveApplication
    {
        [SetUp]
        public void SetUp()
        {
            Runner.SqlCompact("Lemonade").Down();
            Runner.SqlCompact("Lemonade").Up();
        }

        [Test]
        public void WhenITryToSaveADuplicateApplication_ThenSaveApplicationExceptionIsThrown()
        {
            var saveApplication = new SaveApplication();
            var application = new ApplicationBuilder()
                .WithName("Test12345")
                .Build();

            saveApplication.Execute(application);

            Assert.Throws<SaveApplicationException>(() => saveApplication.Execute(application));
        }
    }
}