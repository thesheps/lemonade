using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.EventHandlers
{
    public class FeatureOverrideHasBeenCreatedHandler : IDomainEventHandler<FeatureOverrideHasBeenCreated>
    {
        public FeatureOverrideHasBeenCreatedHandler(INotifyClients notifyClients)
        {
            _notifyClients = notifyClients;
        }

        public void Handle(FeatureOverrideHasBeenCreated @event)
        {
            _notifyClients.AddFeatureOverride(@event);
        }

        private readonly INotifyClients _notifyClients;
    }
}