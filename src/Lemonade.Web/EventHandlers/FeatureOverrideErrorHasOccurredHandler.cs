using Lemonade.Web.Core.Events;
using Lemonade.Web.Infrastructure;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.EventHandlers
{
    public class FeatureOverrideErrorHasOccurredHandler : IDomainEventHandler<FeatureOverrideErrorHasOccurred>
    {
        public FeatureOverrideErrorHasOccurredHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(FeatureOverrideErrorHasOccurred @event)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.logFeatureOverrideError(@event);
        }

        private readonly IConnectionManager _connectionManager;
    }
}