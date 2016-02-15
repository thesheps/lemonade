using Lemonade.Web.Events;
using Lemonade.Web.Infrastructure;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.EventHandlers
{
    public class FeatureOverrideHasBeenDeletedHandler : IDomainEventHandler<FeatureOverrideHasBeenDeleted>
    {
        public FeatureOverrideHasBeenDeletedHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(FeatureOverrideHasBeenDeleted @event)
        {
            var hubContext = _connectionManager.GetHubContext<FeatureHub>();
            hubContext.Clients.All.removeFeatureOverride(@event);
        }

        private readonly IConnectionManager _connectionManager;
    }
}