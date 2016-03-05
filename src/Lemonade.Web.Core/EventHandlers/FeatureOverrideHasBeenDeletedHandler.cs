using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.EventHandlers
{
    public class FeatureOverrideHasBeenDeletedHandler : IDomainEventHandler<FeatureOverrideHasBeenDeleted>
    {
        public FeatureOverrideHasBeenDeletedHandler(INotifyClients notifyClients)
        {
            _notifyClients = notifyClients;
        }

        public void Handle(FeatureOverrideHasBeenDeleted @event)
        {
            _notifyClients.RemoveFeatureOverride(@event);
        }

        private readonly INotifyClients _notifyClients;
    }
}