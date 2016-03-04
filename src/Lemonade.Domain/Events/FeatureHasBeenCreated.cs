using Lemonade.Data.Entities;
using Lemonade.Domain.Infrastructure;

namespace Lemonade.Domain.Events
{
    public class FeatureHasBeenCreated : IDomainEvent
    {
        public int FeatureId { get; }
        public int ApplicationId { get; }
        public string Name { get; }
        public bool IsEnabled { get; }
        public FeatureOverride[] FeatureOverrides { get; }

        public FeatureHasBeenCreated(int featureId, int applicationId, string name, bool isEnabled)
        {
            FeatureId = featureId;
            ApplicationId = applicationId;
            Name = name;
            IsEnabled = isEnabled;
            FeatureOverrides = new FeatureOverride[0];
        }
    }
}