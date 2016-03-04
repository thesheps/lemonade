using Lemonade.Domain.Infrastructure;

namespace Lemonade.Domain.Events
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