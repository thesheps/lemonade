using System;
using Lemonade.Sql.Queries;

namespace Lemonade.Sql
{
    public class SqlFeatureResolver : IFeatureResolver
    {
        public SqlFeatureResolver() : this(new GetFeatureByName())
        {
        }

        public SqlFeatureResolver(string connectionString) : this(new GetFeatureByName(connectionString))
        {
        }

        public SqlFeatureResolver(GetFeatureByName getFeatureByName)
        {
            _getFeatureByName = getFeatureByName;
        }

        public bool? Get(string value)
        {
            var feature = _getFeatureByName.Execute(value);
            return feature.IsEnabled && feature.ExpirationDays.HasValue && DateTime.Now <= feature.StartDate.AddDays(feature.ExpirationDays.Value);
        }

        private readonly GetFeatureByName _getFeatureByName;
    }
}