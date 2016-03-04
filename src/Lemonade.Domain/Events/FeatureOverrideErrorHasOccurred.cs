using Lemonade.Domain.Infrastructure;

namespace Lemonade.Domain.Events
{
    public class FeatureOverrideErrorHasOccurred : IDomainEvent
    {
        public int FeatureOverrideId { get; }

        public FeatureOverrideErrorHasOccurred(int featureOverrideId)
        {
            FeatureOverrideId = featureOverrideId;
        }
    }
}