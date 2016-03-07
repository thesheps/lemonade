using Lemonade.Builders;
using Lemonade.Data.Exceptions;
using Lemonade.Fakes;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using NUnit.Framework;

namespace Lemonade.Sql.Tests
{
    public class GivenCreateConfiguration
    {
        [SetUp]
        public void SetUp()
        {
            Runner.SqlCompact("Lemonade").Down();
            Runner.SqlCompact("Lemonade").Up();
        }

        [Test]
        public void WhenITryToSaveADuplicateConfiguration_ThenSaveConfigurationExceptionIsThrown()
        {
            var saveConfiguration = new CreateConfigurationFake();
            var saveApplication = new CreateApplicationFake();
            var getApplicationByName = new GetApplicationByName();

            var application = new ApplicationBuilder()
                .WithName("Test12345")
                .Build();

            saveApplication.Execute(application);
            application = getApplicationByName.Execute(application.Name);

            var configuration = new ConfigurationBuilder()
                .WithName("MyTestFeature")
                .WithValue("Hello World")
                .WithApplication(application).Build();

            saveConfiguration.Execute(configuration);

            Assert.Throws<CreateConfigurationException>(() => saveConfiguration.Execute(configuration));
        }
    }
}