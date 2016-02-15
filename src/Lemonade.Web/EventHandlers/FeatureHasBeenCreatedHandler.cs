using Lemonade.Web.Events;
using Lemonade.Web.Infrastructure;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.EventHandlers
{
    public class FeatureHasBeenCreatedHandler : IDomainEventHandler<FeatureHasBeenCreated>
    {
        public FeatureHasBeenCreatedHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(FeatureHasBeenCreated @event)
        {
            var hubContext = _connectionManager.GetHubContext<FeatureHub>();
            hubContext.Clients.All.addFeature(@event);
        }

        private readonly IConnectionManager _connectionManager;
    }
}