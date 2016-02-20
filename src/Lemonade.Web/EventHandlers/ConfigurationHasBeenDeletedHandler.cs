using Lemonade.Web.Events;
using Lemonade.Web.Infrastructure;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.EventHandlers
{
    public class ConfigurationHasBeenDeletedHandler : IDomainEventHandler<ConfigurationHasBeenDeleted>
    {
        public ConfigurationHasBeenDeletedHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(ConfigurationHasBeenDeleted @event)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.removeConfiguration(@event);
        }

        private readonly IConnectionManager _connectionManager;
    }
}