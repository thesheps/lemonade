using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.EventHandlers
{
    public class ApplicationHasBeenUpdatedHandler : IDomainEventHandler<ApplicationHasBeenUpdated>
    {
        public ApplicationHasBeenUpdatedHandler(INotifyClients notifyClients)
        {
            _notifyClients = notifyClients;
        }

        public void Handle(ApplicationHasBeenUpdated @event)
        {
            _notifyClients.UpdateApplication(@event);
        }

        private readonly INotifyClients _notifyClients;
    }
}