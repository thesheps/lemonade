using System.Linq;
using Lemonade.Web.Core.Events;
using Nancy.TinyIoc;

namespace Lemonade.Web.Services
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        public DomainEventDispatcher(TinyIoCContainer container)
        {
            _container = container;
        }

        public void Dispatch<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            _container.ResolveAll<IDomainEventHandler<TEvent>>().ToList().ForEach(h => h.Handle(@event));
        }

        private readonly TinyIoCContainer _container;
    }
}