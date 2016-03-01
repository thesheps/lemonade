using Lemonade.Web.Events;
using Lemonade.Web.Infrastructure;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.EventHandlers
{
    public class ResourceHasBeenCreatedHandler : IDomainEventHandler<ResourceHasBeenCreated>
    {
        public ResourceHasBeenCreatedHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(ResourceHasBeenCreated @event)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.addResource(@event);
        }

        private readonly IConnectionManager _connectionManager;
    }
}