using Lemonade.Web.Contracts;

namespace Lemonade.Web.Mappers
{
    public static class FeatureMapper
    {
        public static Feature ToContract(this Data.Entities.Feature feature)
        {
            return new Feature
            {
                FeatureId = feature.FeatureId,
                Name = feature.Name,
                Application = feature.Application.ToContract(),
                IsEnabled = feature.IsEnabled,
            };
        }

        public static Data.Entities.Feature ToDomain(this Feature feature)
        {
            return new Data.Entities.Feature
            {
                FeatureId = feature.FeatureId,
                ApplicationId = feature.ApplicationId,
                Name = feature.Name,
                IsEnabled = feature.IsEnabled,
            };
        }
    }
}