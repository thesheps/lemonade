using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Events;

namespace Lemonade.Web.Core.CommandHandlers
{
    public class DeleteConfigurationCommandHandler : ICommandHandler<DeleteConfigurationCommand>
    {
        public DeleteConfigurationCommandHandler(IDomainEventDispatcher eventDispatcher, IDeleteConfiguration deleteConfiguration)
        {
            _eventDispatcher = eventDispatcher;
            _deleteConfiguration = deleteConfiguration;
        }

        public void Handle(DeleteConfigurationCommand command)
        {
            try
            {
                _deleteConfiguration.Execute(command.ConfigurationId);
                _eventDispatcher.Dispatch(new ConfigurationHasBeenDeleted(command.ConfigurationId));
            }
            catch (DeleteConfigurationException exception)
            {
                _eventDispatcher.Dispatch(new ConfigurationErrorHasOccurred(exception.Message));
                throw;
            }
        }

        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly IDeleteConfiguration _deleteConfiguration;
    }
}