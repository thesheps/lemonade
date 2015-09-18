namespace Lemonade.Web.Mappers
{
    public static class FeatureMapper
    {
        public static Contracts.Feature ToContract(this Data.Entities.Feature feature)
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

        public static Models.FeatureModel ToModel(this Data.Entities.Feature feature)
        {
            return new Models.FeatureModel
            {
                Id = feature.FeatureId,
                FeatureName = feature.Name,
                ApplicationId = feature.Application.ApplicationId,
                ExpirationDays = feature.ExpirationDays,
                IsEnabled = feature.IsEnabled,
                StartDate = feature.StartDate
            };
        }

        public static Data.Entities.Feature ToEntity(this Contracts.Feature feature)
        {
            return new Data.Entities.Feature
            {
                FeatureId = feature.Id,
                Name = feature.Name,
                Application = feature.Application.ToEntity(),
                ExpirationDays = feature.ExpirationDays,
                IsEnabled = feature.IsEnabled,
                StartDate = feature.StartDate
            };
        }
    }
}