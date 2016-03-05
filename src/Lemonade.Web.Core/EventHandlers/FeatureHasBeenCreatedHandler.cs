using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.EventHandlers
{
    public class FeatureHasBeenCreatedHandler : IDomainEventHandler<FeatureHasBeenCreated>
    {
        public FeatureHasBeenCreatedHandler(INotifyClients notifyClients)
        {
            _notifyClients = notifyClients;
        }

        public void Handle(FeatureHasBeenCreated @event)
        {
            _notifyClients.AddFeature(@event);
        }

        private readonly INotifyClients _notifyClients;
    }
}