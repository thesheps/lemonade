using System.Linq;
using Lemonade.Data.Entities;

namespace Lemonade.Web.Core.Mappers
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
                IsEnabled = feature.IsEnabled,
                FeatureOverrides = feature.FeatureOverrides?.Select(f => f.ToContract()).ToList()
            };
        }

        public static Feature ToEntity(this Contracts.Feature feature)
        {
            return new Feature
            {
                FeatureId = feature.FeatureId,
                ApplicationId = feature.ApplicationId,
                Name = feature.Name,
                IsEnabled = feature.IsEnabled,
            };
        }
    }
}