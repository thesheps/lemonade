using Lemonade.Data.Commands;
using Lemonade.Data.Entities;
using Lemonade.Data.Exceptions;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Events;

namespace Lemonade.Web.Core.CommandHandlers
{
    public class UpdateConfigurationCommandHandler : ICommandHandler<UpdateConfigurationCommand>
    {
        public UpdateConfigurationCommandHandler(IDomainEventDispatcher eventDispatcher, IUpdateConfiguration updateConfiguration)
        {
            _eventDispatcher = eventDispatcher;
            _updateConfiguration = updateConfiguration;
        }

        public void Handle(UpdateConfigurationCommand command)
        {
            var configuration = new Configuration { Name = command.Name, Value = command.Value, ConfigurationId = command.ConfigurationId };

            try
            {
                _updateConfiguration.Execute(configuration);
                _eventDispatcher.Dispatch(new ConfigurationHasBeenUpdated(configuration.ConfigurationId, configuration.Name, configuration.Value));
            }
            catch (UpdateConfigurationException ex)
            {
                _eventDispatcher.Dispatch(new ConfigurationErrorHasOccurred(ex.Message));
                throw;
            }
        }

        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly IUpdateConfiguration _updateConfiguration;
    }
}