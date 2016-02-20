using Lemonade.Web.Events;
using Lemonade.Web.Infrastructure;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.EventHandlers
{
    public class ConfigurationErrorHasOccurredHandler : IDomainEventHandler<ConfigurationErrorHasOccurred>
    {
        public ConfigurationErrorHasOccurredHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(ConfigurationErrorHasOccurred @event)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.logConfigurationError(@event);
        }

        private readonly IConnectionManager _connectionManager;
    }
}