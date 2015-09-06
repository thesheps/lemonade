using Lemonade.Data.Entities;
using Lemonade.Web.Models;

namespace Lemonade.Web.Mappers
{
    public static class FeatureMapper
    {
        public static FeatureModel ToModel(this Feature feature)
        {
            return new FeatureModel
            {
                Id = feature.Id,
                Name = feature.Name,
                Application = feature.Application,
                ExpirationDays = feature.ExpirationDays,
                IsEnabled = feature.IsEnabled,
                StartDate = feature.StartDate
            };
        }

        public static Feature ToEntity(this FeatureModel feature)
        {
            return new Feature
            {
                Id = feature.Id,
                Name = feature.Name,
                Application = feature.Application,
                ExpirationDays = feature.ExpirationDays,
                IsEnabled = feature.IsEnabled,
                StartDate = feature.StartDate
            };
        }
    }
}