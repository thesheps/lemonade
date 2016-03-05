using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.EventHandlers
{
    public class ApplicationErrorHasOccurredHandler : IDomainEventHandler<ApplicationErrorHasOccurred>
    {
        public ApplicationErrorHasOccurredHandler(INotifyClients notifyClients)
        {
            _notifyClients = notifyClients;
        }

        public void Handle(ApplicationErrorHasOccurred @event)
        {
            _notifyClients.LogApplicationError(@event);
        }

        private readonly INotifyClients _notifyClients;
    }
}