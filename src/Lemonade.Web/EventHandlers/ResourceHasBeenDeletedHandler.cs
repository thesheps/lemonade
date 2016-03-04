using Lemonade.Web.Core.Events;
using Lemonade.Web.Infrastructure;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.EventHandlers
{
    public class ResourceHasBeenDeletedHandler : IDomainEventHandler<ResourceHasBeenDeleted>
    {
        public ResourceHasBeenDeletedHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(ResourceHasBeenDeleted @event)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.removeResource(@event);
        }

        private readonly IConnectionManager _connectionManager;
    }
}