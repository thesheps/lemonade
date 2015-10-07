using Lemonade.Core.Events;
using Lemonade.Web.Infrastructure;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.EventHandlers
{
    public class ApplicationHasBeenCreatedHandler : IDomainEventHandler<ApplicationHasBeenCreated>
    {
        public ApplicationHasBeenCreatedHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(ApplicationHasBeenCreated @event)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.addApplication(@event);
        }

        private readonly IConnectionManager _connectionManager;
    }
}