using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.EventHandlers
{
    public class ConfigurationHasBeenDeletedHandler : IDomainEventHandler<ConfigurationHasBeenDeleted>
    {
        public ConfigurationHasBeenDeletedHandler(INotifyClients notifyClients)
        {
            _notifyClients = notifyClients;
        }

        public void Handle(ConfigurationHasBeenDeleted @event)
        {
            _notifyClients.RemoveConfiguration(@event);
        }

        private readonly INotifyClients _notifyClients;
    }
}