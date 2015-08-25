using System;
using Lemonade.Services;
using Lemonade.SqlServer.Queries;

namespace Lemonade.SqlServer
{
    public class FeatureResolver : IFeatureResolver
    {
        public bool? Get(string value)
        {
            var feature = new GetFeatureByName().Execute(value);
            return feature.ExpirationDays.HasValue && DateTime.Now > feature.StartDate.AddDays(feature.ExpirationDays.Value);
        }
    }
}