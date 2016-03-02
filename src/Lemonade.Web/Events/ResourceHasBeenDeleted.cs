namespace Lemonade.Web.Events
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