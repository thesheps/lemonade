using Lemonade.Data.Entities;

namespace Lemonade.Web.Core.Mappers
{
    public static class FeatureOverrideMapper
    {
        public static Contracts.FeatureOverride ToContract(this FeatureOverride featureOverride)
        {
            return new Contracts.FeatureOverride
            {
                FeatureOverrideId = featureOverride.FeatureOverrideId,
                FeatureId = featureOverride.FeatureId,
                Hostname = featureOverride.Hostname,
                IsEnabled = featureOverride.IsEnabled
            };
        }

        public static FeatureOverride ToEntity(this Contracts.FeatureOverride featureOverride)
        {
            return new FeatureOverride
            {
                FeatureOverrideId = featureOverride.FeatureOverrideId,
                FeatureId = featureOverride.FeatureId,
                Hostname = featureOverride.Hostname,
                IsEnabled = featureOverride.IsEnabled,
            };
        }
    }
}