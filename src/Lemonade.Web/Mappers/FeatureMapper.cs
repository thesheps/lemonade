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
                ExpirationDays = feature.ExpirationDays,
                IsEnabled = feature.IsEnabled,
                StartDate = feature.StartDate
            };
        }

        public static Data.Entities.Feature ToDomain(this Feature feature)
        {
            return new Data.Entities.Feature
            {
                FeatureId = feature.FeatureId,
                ApplicationId = feature.ApplicationId,
                Name = feature.Name,
                ExpirationDays = feature.ExpirationDays,
                IsEnabled = feature.IsEnabled,
                StartDate = feature.StartDate
            };
        }
    }
}