using Lemonade.Web.Core.Events;
using Lemonade.Web.Infrastructure;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.EventHandlers
{
    public class ConfigurationHasBeenCreatedHandler : IDomainEventHandler<ConfigurationHasBeenCreated>
    {
        public ConfigurationHasBeenCreatedHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(ConfigurationHasBeenCreated @event)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.addConfiguration(@event);
        }

        private readonly IConnectionManager _connectionManager;
    }
}