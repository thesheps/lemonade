using Lemonade.Web.Models;

namespace Lemonade.Web.Mappers
{
    public static class FeatureMapper
    {
        public static FeatureModel ToModel(this Data.Entities.Feature feature)
        {
            return new FeatureModel
            {
                Name = feature.Name,
                ExpirationDays = feature.ExpirationDays,
                IsEnabled = feature.IsEnabled,
                StartDate = feature.StartDate
            };
        }
    }
}