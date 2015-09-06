using System;
using Lemonade.Data.Queries;

namespace Lemonade.Data
{
    public class FeatureResolver : IFeatureResolver
    {
        public FeatureResolver(IGetFeatureByName getFeatureByName)
        {
            _getFeatureByName = getFeatureByName;
        }

        public bool? Get(string value)
        {
            var feature = _getFeatureByName.Execute(value);
            return feature.ExpirationDays.HasValue && DateTime.Now > feature.StartDate.AddDays(feature.ExpirationDays.Value);
        }

        private readonly IGetFeatureByName _getFeatureByName;
    }
}