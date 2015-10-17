using Lemonade.Web.Contracts;

namespace Lemonade.Web.Mappers
{
    public static class FeatureOverrideMapper
    {
        public static FeatureOverride ToContract(this Data.Entities.FeatureOverride feature)
        {
            return new FeatureOverride
            {
                FeatureOverrideId = feature.FeatureOverrideId,
                FeatureId = feature.FeatureId,
                Hostname = feature.Hostname,
                IsEnabled = feature.IsEnabled
            };
        }
    }
}