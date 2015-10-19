using Lemonade.Web.Contracts;

namespace Lemonade.Web.Mappers
{
    public static class FeatureOverrideMapper
    {
        public static FeatureOverride ToContract(this Data.Entities.FeatureOverride featureOverride)
        {
            return new FeatureOverride
            {
                FeatureOverrideId = featureOverride.FeatureOverrideId,
                FeatureId = featureOverride.FeatureId,
                Hostname = featureOverride.Hostname,
                IsEnabled = featureOverride.IsEnabled
            };
        }

        public static Data.Entities.FeatureOverride ToEntity(this FeatureOverride featureOverride)
        {
            return new Data.Entities.FeatureOverride
            {
                FeatureOverrideId = featureOverride.FeatureOverrideId,
                FeatureId = featureOverride.FeatureId,
                Hostname = featureOverride.Hostname,
                IsEnabled = featureOverride.IsEnabled,
            };
        }
    }
}