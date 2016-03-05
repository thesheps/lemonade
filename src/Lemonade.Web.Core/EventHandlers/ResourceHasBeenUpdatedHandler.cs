using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.EventHandlers
{
    public class ResourceHasBeenUpdatedHandler : IDomainEventHandler<ResourceHasBeenUpdated>
    {
        public ResourceHasBeenUpdatedHandler(INotifyClients notifyClients)
        {
            _notifyClients = notifyClients;
        }

        public void Handle(ResourceHasBeenUpdated @event)
        {
            _notifyClients.UpdateResource(@event);
        }

        private readonly INotifyClients _notifyClients;
    }
}