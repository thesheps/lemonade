using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.EventHandlers
{
    public class ConfigurationErrorHasOccurredHandler : IDomainEventHandler<ConfigurationErrorHasOccurred>
    {
        public ConfigurationErrorHasOccurredHandler(INotifyClients notifyClients)
        {
            _notifyClients = notifyClients;
        }

        public void Handle(ConfigurationErrorHasOccurred @event)
        {
            _notifyClients.LogConfigurationError(@event);
        }

        private readonly INotifyClients _notifyClients;
    }
}