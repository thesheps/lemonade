using Lemonade.Core.Events;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.EventHandlers
{
    public class FeatureHasBeenSavedHandler : IDomainEventHandler<FeatureHasBeenSaved>
    {
        public FeatureHasBeenSavedHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(FeatureHasBeenSaved @event)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.addFeature(@event);
        }

        private readonly IConnectionManager _connectionManager;
    }
}