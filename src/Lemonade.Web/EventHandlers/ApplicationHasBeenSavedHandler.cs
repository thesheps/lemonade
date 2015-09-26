using Lemonade.Core.Events;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.EventHandlers
{
    public class ApplicationHasBeenSavedHandler : IDomainEventHandler<ApplicationHasBeenSaved>
    {
        public ApplicationHasBeenSavedHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(ApplicationHasBeenSaved @event)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.addApplication(@event);
        }

        private readonly IConnectionManager _connectionManager;
    }
}