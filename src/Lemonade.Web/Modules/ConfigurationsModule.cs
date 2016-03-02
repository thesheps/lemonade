using System.Collections.Generic;
using System.Linq;
using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;
using Lemonade.Data.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Events;
using Lemonade.Web.Mappers;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class ConfigurationsModule : NancyModule
    {
        public ConfigurationsModule(IGetConfigurationByNameAndApplication getConfigurationByNameAndApplication, IGetAllConfigurationsByApplicationId getAllConfigurationsByApplicationId, ICreateConfiguration createConfiguration, IUpdateConfiguration updateConfiguration, IDeleteConfiguration deleteConfiguration)
        {
            _getConfigurationByNameAndApplication = getConfigurationByNameAndApplication;
            _getAllConfigurationsByApplicationId = getAllConfigurationsByApplicationId;
            _createConfiguration = createConfiguration;
            _updateConfiguration = updateConfiguration;
            _deleteConfiguration = deleteConfiguration;
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
                var configuration = this.Bind<Configuration>().ToEntity();
                _createConfiguration.Execute(configuration);
                DomainEvents.Raise(new ConfigurationHasBeenCreated(configuration.ApplicationId, configuration.Name, configuration.Value));

                return HttpStatusCode.OK;
            }
            catch (CreateConfigurationException exception)
            {
                DomainEvents.Raise(new ConfigurationErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private HttpStatusCode PutConfiguration()
        {
            try
            {
                var configuration = this.Bind<Configuration>().ToEntity();
                _updateConfiguration.Execute(configuration);
                DomainEvents.Raise(new ConfigurationHasBeenUpdated(configuration.ApplicationId, configuration.Name));

                return HttpStatusCode.OK;
            }
            catch (UpdateConfigurationException exception)
            {
                DomainEvents.Raise(new ConfigurationErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private HttpStatusCode DeleteConfiguration()
        {
            int configurationId;
            int.TryParse(Request.Query["id"].Value as string, out configurationId);

            try
            {
                _deleteConfiguration.Execute(configurationId);
                DomainEvents.Raise(new ConfigurationHasBeenDeleted(configurationId));
                return HttpStatusCode.OK;
            }
            catch (DeleteConfigurationException exception)
            {
                DomainEvents.Raise(new ConfigurationErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private readonly IGetConfigurationByNameAndApplication _getConfigurationByNameAndApplication;
        private readonly IGetAllConfigurationsByApplicationId _getAllConfigurationsByApplicationId;
        private readonly ICreateConfiguration _createConfiguration;
        private readonly IUpdateConfiguration _updateConfiguration;
        private readonly IDeleteConfiguration _deleteConfiguration;
    }
}