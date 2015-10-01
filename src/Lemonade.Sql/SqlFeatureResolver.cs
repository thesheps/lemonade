using System;
using Lemonade.Core;
using Lemonade.Sql.Queries;

namespace Lemonade.Sql
{
    public class SqlFeatureResolver : IFeatureResolver
    {
        public SqlFeatureResolver() : this(new GetFeatureByNameAndApplication())
        {
        }

        public SqlFeatureResolver(string connectionString) : this(new GetFeatureByNameAndApplication(connectionString))
        {
        }

        public SqlFeatureResolver(GetFeatureByNameAndApplication getFeatureByNameAndApplication)
        {
            _getFeatureByNameAndApplication = getFeatureByNameAndApplication;
        }

        public bool Get(string featureName)
        {
            var applicationName = AppDomain.CurrentDomain.FriendlyName;
            var feature = _getFeatureByNameAndApplication.Execute(featureName, applicationName);

            if (feature == null) return false;

            return feature.IsEnabled && feature.ExpirationDays.HasValue && DateTime.Now <= feature.StartDate.AddDays(feature.ExpirationDays.Value);
        }

        private readonly GetFeatureByNameAndApplication _getFeatureByNameAndApplication;
    }
}