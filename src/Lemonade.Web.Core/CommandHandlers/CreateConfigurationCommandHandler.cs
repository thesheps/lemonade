using Lemonade.Data.Commands;
using Lemonade.Data.Entities;
using Lemonade.Data.Exceptions;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.CommandHandlers
{
    public class CreateConfigurationCommandHandler : ICommandHandler<CreateConfigurationCommand>
    {
        public CreateConfigurationCommandHandler(IDomainEventDispatcher eventDispatcher, ICreateConfiguration createConfiguration)
        {
            _eventDispatcher = eventDispatcher;
            _createConfiguration = createConfiguration;
        }

        public void Handle(CreateConfigurationCommand command)
        {
            var configuration = new Configuration { ApplicationId = command.ApplicationId, Name = command.Name, Value = command.Value };

            try
            {
                _createConfiguration.Execute(configuration);
                _eventDispatcher.Dispatch(new ConfigurationHasBeenCreated(configuration.ConfigurationId, configuration.Name, configuration.Value));
            }
            catch (CreateConfigurationException ex)
            {
                _eventDispatcher.Dispatch(new ConfigurationErrorHasOccurred(ex.Message));
                throw;
            }
        }

        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly ICreateConfiguration _createConfiguration;
    }
}