using Lemonade.Web.Events;
using Lemonade.Web.Infrastructure;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.EventHandlers
{
    public class FeatureOverrideHasBeenUpdatedHandler : IDomainEventHandler<FeatureOverrideHasBeenUpdated>
    {
        public FeatureOverrideHasBeenUpdatedHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(FeatureOverrideHasBeenUpdated @event)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.updateFeatureOverride(@event);
        }

        private readonly IConnectionManager _connectionManager;
    }
}