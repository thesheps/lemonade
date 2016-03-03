using Lemonade.Web.Infrastructure;

namespace Lemonade.Web.Events
{
    public class FeatureOverrideHasBeenCreated : IDomainEvent
    {
        public int FeatureOverrideId { get; }
        public int FeatureId { get; }
        public string Hostname { get; }
        public bool IsEnabled { get; }

        public FeatureOverrideHasBeenCreated(int featureOverrideId, int featureId, string hostname, bool isEnabled)
        {
            FeatureOverrideId = featureOverrideId;
            FeatureId = featureId;
            Hostname = hostname;
            IsEnabled = isEnabled;
        }
    }
}