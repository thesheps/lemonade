namespace Lemonade.Web.Core.Events
{
    public class FeatureOverrideHasBeenUpdated : IDomainEvent
    {
        public int FeatureOverrideId { get; }
        public int FeatureId { get; }
        public string Hostname { get; }
        public bool IsEnabled { get; }

        public FeatureOverrideHasBeenUpdated(int featureOverrideId, int featureId, string hostname, bool isEnabled)
        {
            FeatureOverrideId = featureOverrideId;
            FeatureId = featureId;
            Hostname = hostname;
            IsEnabled = isEnabled;
        }
    }
}