using Lemonade.Web.Events;
using Lemonade.Web.Infrastructure;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.EventHandlers
{
    public class ApplicationHasBeenUpdatedHandler : IDomainEventHandler<ApplicationHasBeenUpdated>
    {
        public ApplicationHasBeenUpdatedHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(ApplicationHasBeenUpdated @event)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.updateApplication(@event);
        }

        private readonly IConnectionManager _connectionManager;
    }
}