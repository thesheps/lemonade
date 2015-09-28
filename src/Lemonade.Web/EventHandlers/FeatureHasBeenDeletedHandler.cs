using Lemonade.Core.Events;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.EventHandlers
{
    public class FeatureHasBeenDeletedHandler : IDomainEventHandler<FeatureHasBeenDeleted>
    {
        public FeatureHasBeenDeletedHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(FeatureHasBeenDeleted @event)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.removeFeature(@event);
        }

        private readonly IConnectionManager _connectionManager;
    }
}