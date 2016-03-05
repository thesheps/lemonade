using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.EventHandlers
{
    public class ConfigurationHasBeenCreatedHandler : IDomainEventHandler<ConfigurationHasBeenCreated>
    {
        public ConfigurationHasBeenCreatedHandler(INotifyClients notifyClients)
        {
            _notifyClients = notifyClients;
        }

        public void Handle(ConfigurationHasBeenCreated @event)
        {
            _notifyClients.AddConfiguration(@event);
        }

        private readonly INotifyClients _notifyClients;
    }
}