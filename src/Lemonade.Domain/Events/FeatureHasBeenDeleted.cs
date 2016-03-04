using Lemonade.Domain.Infrastructure;

namespace Lemonade.Domain.Events
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