using System.Collections.Generic;
using System.Linq;
using Lemonade.Data.Exceptions;
using Lemonade.Data.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Mappers;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class ConfigurationsModule : NancyModule
    {
        public ConfigurationsModule(ICommandDispatcher commandDispatcher, IGetConfigurationByNameAndApplication getConfigurationByNameAndApplication, IGetAllConfigurationsByApplicationId getAllConfigurationsByApplicationId)
        {
            _commandDispatcher = commandDispatcher;
            _getConfigurationByNameAndApplication = getConfigurationByNameAndApplication;
            _getAllConfigurationsByApplicationId = getAllConfigurationsByApplicationId;
            Get["/api/configurations"] = p => GetConfigurations();
            Get["/api/configuration"] = p => GetConfiguration();
            Post["/api/configurations"] = p => PostConfiguration();
            Put["/api/configurations"] = p => PutConfiguration();
            Delete["/api/configurations"] = p => DeleteConfiguration();
        }

        private Configuration GetConfiguration()
        {
            var configurationName = Request.Query["configuration"].Value as string;
            var applicationName = Request.Query["application"].Value as string;
            var configuration = _getConfigurationByNameAndApplication.Execute(configurationName, applicationName);

            if (configuration == null) throw new ConfigurationDoesNotExistException();

            return configuration.ToContract();
        }

        private IList<Configuration> GetConfigurations()
        {
            int applicationId;
            int.TryParse(Request.Query["applicationId"].Value as string, out applicationId);

            var configurations = _getAllConfigurationsByApplicationId.Execute(applicationId);

            return configurations.Select(f => f.ToContract()).ToList();
        }

        private HttpStatusCode PostConfiguration()
        {
            try
            {
                var configuration = this.Bind<Configuration>();
                _commandDispatcher.Dispatch(new CreateConfigurationCommand(configuration.ApplicationId, configuration.Name, configuration.Value));

                return HttpStatusCode.OK;
            }
            catch (CreateConfigurationException)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        private HttpStatusCode PutConfiguration()
        {
            try
            {
                var configuration = this.Bind<Configuration>();
                _commandDispatcher.Dispatch(new UpdateConfigurationCommand(configuration.ConfigurationId, configuration.Name, configuration.Value));

                return HttpStatusCode.OK;
            }
            catch (UpdateConfigurationException)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        private HttpStatusCode DeleteConfiguration()
        {
            int configurationId;
            int.TryParse(Request.Query["id"].Value as string, out configurationId);

            try
            {
                _commandDispatcher.Dispatch(new DeleteConfigurationCommand(configurationId));
                return HttpStatusCode.OK;
            }
            catch (DeleteConfigurationException)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IGetConfigurationByNameAndApplication _getConfigurationByNameAndApplication;
        private readonly IGetAllConfigurationsByApplicationId _getAllConfigurationsByApplicationId;
    }
}