using Lemonade.Web.Events;
using Lemonade.Web.Infrastructure;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.EventHandlers
{
    public class FeatureHasBeenUpdatedHandler : IDomainEventHandler<FeatureHasBeenUpdated>
    {
        public FeatureHasBeenUpdatedHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(FeatureHasBeenUpdated @event)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.updateFeature(@event);
        }

        private readonly IConnectionManager _connectionManager;
    }
}