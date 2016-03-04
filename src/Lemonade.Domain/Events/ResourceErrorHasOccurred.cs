using Lemonade.Domain.Infrastructure;

namespace Lemonade.Domain.Events
{
    public class ResourceErrorHasOccurred : IDomainEvent
    {
        public string Message { get; private set; }

        public ResourceErrorHasOccurred(string message)
        {
            Message = message;
        }
    }
}