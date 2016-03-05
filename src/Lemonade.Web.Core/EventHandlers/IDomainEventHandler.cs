using Lemonade.Web.Core.Events;

namespace Lemonade.Web.Core.EventHandlers
{
    public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
    {
        void Handle(TEvent @event);
    }
}