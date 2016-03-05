using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.EventHandlers
{
    public class FeatureHasBeenUpdatedHandler : IDomainEventHandler<FeatureHasBeenUpdated>
    {
        public FeatureHasBeenUpdatedHandler(INotifyClients notifyClients)
        {
            _notifyClients = notifyClients;
        }

        public void Handle(FeatureHasBeenUpdated @event)
        {
            _notifyClients.UpdateFeature(@event);
        }

        private readonly INotifyClients _notifyClients;
    }
}