using Lemonade.Domain.Infrastructure;

namespace Lemonade.Domain.Events
{
    public class FeatureOverrideHasBeenUpdated : IDomainEvent
    {
        public int FeatureOverrideId { get; private set; }
        public int FeatureId { get; private set; }
        public string Hostname { get; private set; }
        public bool IsEnabled { get; private set; }

        public FeatureOverrideHasBeenUpdated(int featureOverrideId, int featureId, string hostname, bool isEnabled)
        {
            FeatureOverrideId = featureOverrideId;
            FeatureId = featureId;
            Hostname = hostname;
            IsEnabled = isEnabled;
        }
    }
}