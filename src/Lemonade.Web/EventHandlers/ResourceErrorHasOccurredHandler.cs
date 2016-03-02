using Lemonade.Web.Events;
using Lemonade.Web.Infrastructure;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.EventHandlers
{
    public class ResourceErrorHasOccurredHandler : IDomainEventHandler<ResourceErrorHasOccurred>
    {
        public ResourceErrorHasOccurredHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(ResourceErrorHasOccurred @event)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.logResourceError(@event);
        }

        private readonly IConnectionManager _connectionManager;
    }
}