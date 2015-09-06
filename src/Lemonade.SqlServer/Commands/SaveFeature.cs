using System.Data.Common;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Entities;

namespace Lemonade.Sql.Commands
{
    public class SaveFeature : ISaveFeature
    {
        public SaveFeature(DbProviderFactory dbProviderFactory, string connectionString)
        {
            _dbProviderFactory = dbProviderFactory;
            _connectionString = connectionString;
        }

        public void Execute(Feature feature)
        {
            using (var cnn = _dbProviderFactory.CreateConnection())
            {
                if (cnn == null) return;

                cnn.ConnectionString = _connectionString;
                cnn.Execute("INSERT INTO Feature (Id, IsEnabled, ExpirationDays, StartDate, Name, Application) " +
                            "VALUES (@Id, @IsEnabled, @ExpirationDays, @StartDate, @Name, @Application)", new
                            {
                                feature.Id, feature.IsEnabled, feature.ExpirationDays, feature.StartDate, feature.Name, feature.Application
                            });
            }
        }

        private readonly DbProviderFactory _dbProviderFactory;
        private readonly string _connectionString;
    }
}