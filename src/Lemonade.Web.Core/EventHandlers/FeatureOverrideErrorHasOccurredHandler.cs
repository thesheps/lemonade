using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.EventHandlers
{
    public class FeatureOverrideErrorHasOccurredHandler : IDomainEventHandler<FeatureOverrideErrorHasOccurred>
    {
        public FeatureOverrideErrorHasOccurredHandler(INotifyClients notifyClients)
        {
            _notifyClients = notifyClients;
        }

        public void Handle(FeatureOverrideErrorHasOccurred @event)
        {
            _notifyClients.LogFeatureOverrideError(@event);
        }

        private readonly INotifyClients _notifyClients;
    }
}