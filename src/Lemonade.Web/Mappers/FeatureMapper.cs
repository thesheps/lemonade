using Lemonade.Core.Domain;
using Lemonade.Web.Models;

namespace Lemonade.Web.Mappers
{
    public static class FeatureMapper
    {
        public static Contracts.Feature ToContract(this Feature feature)
        {
            return new Contracts.Feature
            {
                Id = feature.FeatureId,
                Name = feature.Name,
                Application = feature.Application.ToContract(),
                ExpirationDays = feature.ExpirationDays,
                IsEnabled = feature.IsEnabled,
                StartDate = feature.StartDate
            };
        }

        public static FeatureModel ToModel(this Feature feature)
        {
            return new FeatureModel
            {
                Id = feature.FeatureId,
                Name = feature.Name,
                ApplicationId = feature.Application.ApplicationId,
                ExpirationDays = feature.ExpirationDays,
                IsEnabled = feature.IsEnabled,
                StartDate = feature.StartDate
            };
        }

        public static Feature ToDomain(this Contracts.Feature feature)
        {
            return new Feature
            {
                FeatureId = feature.Id,
                ApplicationId = feature.ApplicationId,
                Application = feature.Application.ToDomain(),
                Name = feature.Name,
                ExpirationDays = feature.ExpirationDays,
                IsEnabled = feature.IsEnabled,
                StartDate = feature.StartDate
            };
        }

        public static Feature ToDomain(this FeatureModel feature)
        {
            return new Feature
            {
                FeatureId = feature.Id,
                ApplicationId = feature.ApplicationId,
                Name = feature.Name,
                ExpirationDays = feature.ExpirationDays,
                IsEnabled = feature.IsEnabled,
                StartDate = feature.StartDate
            };
        }
    }
}