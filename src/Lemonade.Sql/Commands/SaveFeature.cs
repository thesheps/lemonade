using System.Configuration;
using System.Data.Common;
using Dapper;

namespace Lemonade.Sql.Commands
{
    public class SaveFeature
    {
        public SaveFeature()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Lemonade"];
            _dbProviderFactory = DbProviderFactories.GetFactory(connectionString.ProviderName);
            _connectionString = connectionString.ConnectionString;
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
                cnn.Execute("INSERT INTO Feature (IsEnabled, ExpirationDays, StartDate, Name, Application) " +
                            "VALUES (@IsEnabled, @ExpirationDays, @StartDate, @Name, @Application)", new
                            {
                                feature.IsEnabled, feature.ExpirationDays, feature.StartDate, feature.Name, feature.Application
                            });
            }
        }

        private readonly DbProviderFactory _dbProviderFactory;
        private readonly string _connectionString;
    }
}