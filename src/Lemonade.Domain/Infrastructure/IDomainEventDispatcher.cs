namespace Lemonade.Domain.Infrastructure
{
    public interface IDomainEventDispatcher
    {
        void Dispatch<TEvent>(TEvent @event) where TEvent : IDomainEvent;
    }
}