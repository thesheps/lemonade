namespace Lemonade.Core.Events
{
    public class FeatureHasBeenDeleted : IDomainEvent
    {
        public int FeatureId { get; }

        public FeatureHasBeenDeleted(int featureId)
        {
            FeatureId = featureId;
        }
    }
}