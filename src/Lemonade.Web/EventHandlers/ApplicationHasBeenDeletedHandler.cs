using Lemonade.Core.Events;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.EventHandlers
{
    public class ApplicationHasBeenDeletedHandler : IDomainEventHandler<ApplicationHasBeenDeleted>
    {
        public ApplicationHasBeenDeletedHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(ApplicationHasBeenDeleted @event)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.removeApplication(@event);
        }

        private readonly IConnectionManager _connectionManager;

    }
}