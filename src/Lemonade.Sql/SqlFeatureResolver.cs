using System;
using Lemonade.Sql.Queries;

namespace Lemonade.Sql
{
    public class SqlFeatureResolver : IFeatureResolver
    {
        public SqlFeatureResolver()
        {
            _getFeatureByName = new GetFeatureByName();
        }

        public SqlFeatureResolver(GetFeatureByName getFeatureByName)
        {
            _getFeatureByName = getFeatureByName;
        }

        public bool? Get(string value)
        {
            var feature = _getFeatureByName.Execute(value);
            return feature.ExpirationDays.HasValue && DateTime.Now > feature.StartDate.AddDays(feature.ExpirationDays.Value);
        }

        private readonly GetFeatureByName _getFeatureByName;
    }
}