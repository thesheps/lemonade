using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.EventHandlers
{
    public class ResourceHasBeenCreatedHandler : IDomainEventHandler<ResourceHasBeenCreated>
    {
        public ResourceHasBeenCreatedHandler(INotifyClients notifyClients)
        {
            _notifyClients = notifyClients;
        }

        public void Handle(ResourceHasBeenCreated @event)
        {
            _notifyClients.AddResource(@event);
        }

        private readonly INotifyClients _notifyClients;
    }
}