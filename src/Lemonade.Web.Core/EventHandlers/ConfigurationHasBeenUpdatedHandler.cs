using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.EventHandlers
{
    public class ConfigurationHasBeenUpdatedHandler : IDomainEventHandler<ConfigurationHasBeenUpdated>
    {
        public ConfigurationHasBeenUpdatedHandler(INotifyClients notifyClients)
        {
            _notifyClients = notifyClients;
        }

        public void Handle(ConfigurationHasBeenUpdated @event)
        {
            _notifyClients.UpdateConfiguration(@event);
        }

        private readonly INotifyClients _notifyClients;
    }
}