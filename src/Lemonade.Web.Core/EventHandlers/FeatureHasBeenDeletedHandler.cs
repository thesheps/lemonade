using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.EventHandlers
{
    public class FeatureHasBeenDeletedHandler : IDomainEventHandler<FeatureHasBeenDeleted>
    {
        public FeatureHasBeenDeletedHandler(INotifyClients notifyClients)
        {
            _notifyClients = notifyClients;
        }

        public void Handle(FeatureHasBeenDeleted @event)
        {
            _notifyClients.RemoveFeature(@event);
        }

        private readonly INotifyClients _notifyClients;
    }
}