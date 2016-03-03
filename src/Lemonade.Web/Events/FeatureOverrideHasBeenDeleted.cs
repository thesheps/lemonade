using Lemonade.Web.Infrastructure;

namespace Lemonade.Web.Events
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