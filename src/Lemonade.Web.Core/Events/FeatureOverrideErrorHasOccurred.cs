namespace Lemonade.Web.Core.Events
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