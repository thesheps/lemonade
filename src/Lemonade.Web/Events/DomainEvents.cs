namespace Lemonade.Web.Events
{
    public static class DomainEvents
    {
        public static IDomainEventDispatcher Dispatcher { get; set; }

        public static void Raise<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            Dispatcher?.Dispatch(@event);
        }
    }
}