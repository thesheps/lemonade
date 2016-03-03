using Lemonade.Web.Infrastructure;

namespace Lemonade.Web.Events
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