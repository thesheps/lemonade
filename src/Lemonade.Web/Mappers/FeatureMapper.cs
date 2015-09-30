using Lemonade.Core.Domain;

namespace Lemonade.Web.Mappers
{
    public static class FeatureMapper
    {
        public static Contracts.Feature ToContract(this Feature feature)
        {
            return new Contracts.Feature
            {
                FeatureId = feature.FeatureId,
                Name = feature.Name,
                Application = feature.Application.ToContract(),
                ExpirationDays = feature.ExpirationDays,
                IsEnabled = feature.IsEnabled,
                StartDate = feature.StartDate
            };
        }

        public static Feature ToDomain(this Contracts.Feature feature)
        {
            return new Feature
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