namespace Lemonade.Web.Mappers
{
    public static class FeatureMapper
    {
        public static Contracts.Feature ToContract(this Data.Entities.Feature feature)
        {
            return new Contracts.Feature
            {
                Id = feature.Id,
                FeatureName = feature.FeatureName,
                ApplicationName = feature.ApplicationName,
                ExpirationDays = feature.ExpirationDays,
                IsEnabled = feature.IsEnabled,
                StartDate = feature.StartDate
            };
        }

        public static Models.FeatureModel ToModel(this Data.Entities.Feature feature)
        {
            return new Models.FeatureModel
            {
                Id = feature.Id,
                FeatureName = feature.FeatureName,
                ApplicationName = feature.ApplicationName,
                ExpirationDays = feature.ExpirationDays,
                IsEnabled = feature.IsEnabled,
                StartDate = feature.StartDate
            };
        }

        public static Data.Entities.Feature ToEntity(this Contracts.Feature feature)
        {
            return new Data.Entities.Feature
            {
                Id = feature.Id,
                FeatureName = feature.FeatureName,
                ApplicationName = feature.ApplicationName,
                ExpirationDays = feature.ExpirationDays,
                IsEnabled = feature.IsEnabled,
                StartDate = feature.StartDate
            };
        }
    }
}