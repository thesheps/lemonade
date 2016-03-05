using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.EventHandlers
{
    public class FeatureOverrideHasBeenUpdatedHandler : IDomainEventHandler<FeatureOverrideHasBeenUpdated>
    {
        public FeatureOverrideHasBeenUpdatedHandler(INotifyClients notifyClients)
        {
            _notifyClients = notifyClients;
        }

        public void Handle(FeatureOverrideHasBeenUpdated @event)
        {
            _notifyClients.UpdateFeatureOverride(@event);
        }

        private readonly INotifyClients _notifyClients;
    }
}