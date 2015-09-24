using Lemonade.Core.Events;

namespace Lemonade.Web.EventHandlers
{
    public class ApplicationHasBeenSavedHandler : IDomainEventHandler<ApplicationHasBeenSaved>
    {
        public void Handle(ApplicationHasBeenSaved @event)
        {
            throw new System.NotImplementedException();
        }
    }
}