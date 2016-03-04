using Lemonade.Web.Core.Events;
using Lemonade.Web.Infrastructure;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.EventHandlers
{
    public class ConfigurationHasBeenUpdatedHandler : IDomainEventHandler<ConfigurationHasBeenUpdated>
    {
        public ConfigurationHasBeenUpdatedHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(ConfigurationHasBeenUpdated @event)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.updateConfiguration(@event);
        }

        private readonly IConnectionManager _connectionManager;
    }
}