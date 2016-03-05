using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.EventHandlers
{
    public class FeatureErrorHasOccurredHandler : IDomainEventHandler<FeatureErrorHasOccurred>
    {
        public FeatureErrorHasOccurredHandler(INotifyClients notifyClients)
        {
            _notifyClients = notifyClients;
        }

        public void Handle(FeatureErrorHasOccurred @event)
        {
            _notifyClients.LogFeatureError(@event);
        }

        private readonly INotifyClients _notifyClients;
    }
}