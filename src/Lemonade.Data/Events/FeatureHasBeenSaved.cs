namespace Lemonade.Core.Events
{
    public class FeatureHasBeenSaved : IDomainEvent
    {
        public int FeatureId { get; }
        public int ApplicationId { get; }
        public string FeatureName { get; }

        public FeatureHasBeenSaved(int featureId, int applicationId, string featureName)
        {
            FeatureId = featureId;
            ApplicationId = applicationId;
            FeatureName = featureName;
        }
    }
}