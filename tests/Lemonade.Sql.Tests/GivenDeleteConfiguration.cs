using Lemonade.Builders;
using Lemonade.Data.Entities;
using Lemonade.Fakes;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using NUnit.Framework;

namespace Lemonade.Sql.Tests
{
    public class GivenDeleteConfiguration
    {
        private CreateConfigurationFake _createConfiguration;
        private CreateApplicationFake _createApplication;
        private DeleteConfiguration _deleteConfiguration;
        private GetConfigurationByNameAndApplication _getConfigurationByNameAndApplication;

        [SetUp]
        public void SetUp()
        {
            _createConfiguration = new CreateConfigurationFake();
            _createApplication = new CreateApplicationFake();
            _deleteConfiguration = new DeleteConfiguration();
            _getConfigurationByNameAndApplication = new GetConfigurationByNameAndApplication();
            Runner.SqlCompact("Lemonade").Down();
            Runner.SqlCompact("Lemonade").Up();
        }

        [Test]
        public void WhenIDeleteAnApplication_ThenItIsNoLongerAvailable()
        {
            var application = new ApplicationBuilder()
                .WithName("Test12345")
                .Build();

            _createApplication.Execute(application);

            var configuration = new Configuration { ApplicationId = application.ApplicationId, Name = "Test12345", Value = "TEST" };
            _createConfiguration.Execute(configuration);

            configuration = _getConfigurationByNameAndApplication.Execute("Test12345", application.Name);

            _deleteConfiguration.Execute(configuration.ConfigurationId);

            configuration = _getConfigurationByNameAndApplication.Execute("Test12345", application.Name);

            Assert.That(configuration, Is.Null);
        }
    }
}