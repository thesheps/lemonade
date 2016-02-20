using Lemonade.Web.Events;
using Lemonade.Web.Infrastructure;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.EventHandlers
{
    public class FeatureErrorHasOccurredHandler : IDomainEventHandler<FeatureErrorHasOccurred>
    {
        public FeatureErrorHasOccurredHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(FeatureErrorHasOccurred @event)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.logFeatureError(@event);
        }

        private readonly IConnectionManager _connectionManager;
    }
}