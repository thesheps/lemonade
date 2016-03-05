using Lemonade.Web.Core.Events;

namespace Lemonade.Web.Core.Services
{
    public interface IDomainEventDispatcher
    {
        void Dispatch<TEvent>(TEvent @event) where TEvent : IDomainEvent;
    }
}