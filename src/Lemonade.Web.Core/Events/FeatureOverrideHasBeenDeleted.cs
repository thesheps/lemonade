namespace Lemonade.Web.Core.Events
{
    public class FeatureOverrideHasBeenDeleted : IDomainEvent
    {
        public int FeatureOverrideId { get; }

        public FeatureOverrideHasBeenDeleted(int featureOverrideId)
        {
            FeatureOverrideId = featureOverrideId;
        }
    }
}