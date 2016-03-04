namespace Lemonade.Web.Core.Events
{
    public interface IDomainEventDispatcher
    {
        void Dispatch<TEvent>(TEvent @event) where TEvent : IDomainEvent;
    }
}