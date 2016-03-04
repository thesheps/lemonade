namespace Lemonade.Web.Core.Events
{
    public class ResourceHasBeenDeleted : IDomainEvent
    {
        public int ResourceId { get; }

        public ResourceHasBeenDeleted(int resourceId)
        {
            ResourceId = resourceId;
        }
    }
}