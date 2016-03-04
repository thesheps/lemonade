using Lemonade.Domain.Infrastructure;

namespace Lemonade.Domain.Events
{
    public class ResourceHasBeenDeleted : IDomainEvent
    {
        public int ResourceId { get; private set; }

        public ResourceHasBeenDeleted(int resourceId)
        {
            ResourceId = resourceId;
        }
    }
}