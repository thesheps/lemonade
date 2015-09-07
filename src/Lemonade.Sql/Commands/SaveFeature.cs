using System.Configuration;
using System.Data.Common;
using Dapper;
using Lemonade.Sql.Exceptions;

namespace Lemonade.Sql.Commands
{
    public class SaveFeature
    {
        public SaveFeature() 
            : this("Lemonade")
        {
        }

        public SaveFeature(string connectionStringName)
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (connectionStringSettings == null)
                throw new ConnectionStringNotFoundException(connectionStringName);

            _dbProviderFactory = DbProviderFactories.GetFactory(connectionStringSettings.ProviderName);
            _connectionString = connectionStringSettings.ConnectionString;
        }

        public SaveFeature(DbProviderFactory dbProviderFactory, string connectionString)
        {
            _dbProviderFactory = dbProviderFactory;
            _connectionString = connectionString;
        }

        public void Execute(Entities.Feature feature)
        {
            using (var cnn = _dbProviderFactory.CreateConnection())
            {
                if (cnn == null) return;

                cnn.ConnectionString = _connectionString;
                cnn.Execute("INSERT INTO Feature (IsEnabled, ExpirationDays, StartDate, FeatureName, ApplicationName) " +
                            "VALUES (@IsEnabled, @ExpirationDays, @StartDate, @FeatureName, @ApplicationName)", new
                            {
                                feature.IsEnabled, feature.ExpirationDays, feature.StartDate, feature.FeatureName, feature.ApplicationName
                            });
            }
        }

        private readonly DbProviderFactory _dbProviderFactory;
        private readonly string _connectionString;
    }
}