using Lemonade.Web.Infrastructure;

namespace Lemonade.Web.Events
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