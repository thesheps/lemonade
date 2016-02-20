using Lemonade.Core.Services;
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

        public bool Resolve(string featureName, string applicationName)
        {
            var feature = _getFeatureByNameAndApplication.Execute(featureName, applicationName);

            return feature != null && feature.IsEnabled;
        }

        private readonly GetFeatureByNameAndApplication _getFeatureByNameAndApplication;
    }
}