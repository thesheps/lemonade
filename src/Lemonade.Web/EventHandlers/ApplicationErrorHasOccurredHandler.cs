using Lemonade.Web.Events;
using Lemonade.Web.Infrastructure;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.EventHandlers
{
    public class ApplicationErrorHasOccurredHandler : IDomainEventHandler<ApplicationErrorHasOccurred>
    {
        public ApplicationErrorHasOccurredHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(ApplicationErrorHasOccurred @event)
        {
            var hubContext = _connectionManager.GetHubContext<ApplicationHub>();
            hubContext.Clients.All.logError(@event);
        }

        private readonly IConnectionManager _connectionManager;
    }
}