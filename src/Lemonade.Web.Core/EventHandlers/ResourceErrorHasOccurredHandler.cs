using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.EventHandlers
{
    public class ResourceErrorHasOccurredHandler : IDomainEventHandler<ResourceErrorHasOccurred>
    {
        public ResourceErrorHasOccurredHandler(INotifyClients notifyClients)
        {
            _notifyClients = notifyClients;
        }

        public void Handle(ResourceErrorHasOccurred @event)
        {
            _notifyClients.LogResourceError(@event);
        }

        private readonly INotifyClients _notifyClients;
    }
}