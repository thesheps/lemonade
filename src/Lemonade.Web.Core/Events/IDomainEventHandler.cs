namespace Lemonade.Web.Core.Events
{
    public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
    {
        void Handle(TEvent @event);
    }
}