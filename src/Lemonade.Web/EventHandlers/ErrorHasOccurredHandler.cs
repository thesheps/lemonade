using Lemonade.Core.Events;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.EventHandlers
{
    public class ErrorHasOccurredHandler : IDomainEventHandler<ErrorHasOccurred>
    {
        public ErrorHasOccurredHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Handle(ErrorHasOccurred @event)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.logError(@event);
        }

        private readonly IConnectionManager _connectionManager;
    }
}