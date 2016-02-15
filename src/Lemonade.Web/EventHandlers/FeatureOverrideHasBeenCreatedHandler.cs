using Lemonade.Web.Events;
using Lemonade.Web.Infrastructure;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.EventHandlers
{
    public class FeatureOverrideHasBeenCreatedHandler : IDomainEventHandler<FeatureOverrideHasBeenCreated>
    {
        public FeatureOverrideHasBeenCreatedHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(FeatureOverrideHasBeenCreated @event)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.addFeatureOverride(@event);
        }

        private readonly IConnectionManager _connectionManager;
    }
}