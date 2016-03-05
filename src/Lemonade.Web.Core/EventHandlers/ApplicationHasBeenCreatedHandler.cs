using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.EventHandlers
{
    public class ApplicationHasBeenCreatedHandler : IDomainEventHandler<ApplicationHasBeenCreated>
    {
        public ApplicationHasBeenCreatedHandler(INotifyClients notifyClients)
        {
            _notifyClients = notifyClients;
        }

        public void Handle(ApplicationHasBeenCreated @event)
        {
            _notifyClients.AddApplication(@event);
        }

        private readonly INotifyClients _notifyClients;
    }
}