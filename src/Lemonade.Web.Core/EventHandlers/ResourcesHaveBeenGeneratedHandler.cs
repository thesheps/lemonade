using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Core.EventHandlers
{
    public class ResourcesHaveBeenGeneratedHandler : IDomainEventHandler<ResourcesHaveBeenGenerated>
    {
        public ResourcesHaveBeenGeneratedHandler(INotifyClients notifyClients)
        {
            _notifyClients = notifyClients;
        }

        public void Handle(ResourcesHaveBeenGenerated @event)
        {
            _notifyClients.LogResourcesGenerated(@event);
        }

        private readonly INotifyClients _notifyClients;
    }
}