using Lemonade.Data.Exceptions;
using Lemonade.Data.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Mappers;
using Nancy;

namespace Lemonade.Web.Modules
{
    public class ConfigurationsModule : NancyModule
    {
        public ConfigurationsModule(IGetConfigurationByNameAndApplication getConfigurationByNameAndApplication)
        {
            _getConfigurationByNameAndApplication = getConfigurationByNameAndApplication;
            Get["/api/configuration"] = p => GetConfiguration();
        }

        private Configuration GetConfiguration()
        {
            var configurationName = Request.Query["configuration"].Value as string;
            var applicationName = Request.Query["application"].Value as string;
            var configuration = _getConfigurationByNameAndApplication.Execute(configurationName, applicationName);

            if (configuration == null) throw new ConfigurationDoesNotExistException();

            return configuration.ToContract();
        }

        private readonly IGetConfigurationByNameAndApplication _getConfigurationByNameAndApplication;
    }
}