using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.EventHandlers
{
    public class ResourceHasBeenDeletedHandler : IDomainEventHandler<ResourceHasBeenDeleted>
    {
        public ResourceHasBeenDeletedHandler(INotifyClients notifyClients)
        {
            _notifyClients = notifyClients;
        }

        public void Handle(ResourceHasBeenDeleted @event)
        {
            _notifyClients.RemoveResource(@event);
        }

        private readonly INotifyClients _notifyClients;
    }
}