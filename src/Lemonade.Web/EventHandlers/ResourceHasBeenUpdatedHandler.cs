using Lemonade.Web.Events;
using Lemonade.Web.Infrastructure;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.EventHandlers
{
    public class ResourceHasBeenUpdatedHandler : IDomainEventHandler<ResourceHasBeenUpdated>
    {
        public ResourceHasBeenUpdatedHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(ResourceHasBeenUpdated @event)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.updateResource(@event);
        }

        private readonly IConnectionManager _connectionManager;
    }
}