namespace Lemonade.Core.Events
{
    public class FeatureHasBeenSaved : IDomainEvent
    {
        public int FeatureId { get; }

        public FeatureHasBeenSaved(int featureId)
        {
            FeatureId = featureId;
        }
    }
}