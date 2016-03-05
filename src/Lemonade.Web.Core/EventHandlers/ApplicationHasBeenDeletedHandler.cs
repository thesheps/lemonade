using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.EventHandlers
{
    public class ApplicationHasBeenDeletedHandler : IDomainEventHandler<ApplicationHasBeenDeleted>
    {
        public ApplicationHasBeenDeletedHandler(INotifyClients notifyClients)
        {
            _notifyClients = notifyClients;
        }

        public void Handle(ApplicationHasBeenDeleted @event)
        {
            _notifyClients.RemoveApplication(@event);
        }

        private readonly INotifyClients _notifyClients;
    }
}