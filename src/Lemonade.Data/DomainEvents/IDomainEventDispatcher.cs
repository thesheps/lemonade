namespace Lemonade.Core.DomainEvents
{
    public interface IDomainEventDispatcher
    {
        void Dispatch<TEvent>(TEvent @event) where TEvent : IDomainEvent;
    }
}