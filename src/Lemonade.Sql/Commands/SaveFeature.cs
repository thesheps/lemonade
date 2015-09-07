using System.Data.Common;
using Dapper;

namespace Lemonade.Sql.Commands
{
    public class SaveFeature : LemonadeConnection
    {
        public SaveFeature()
        {
        }

        public SaveFeature(string connectionStringName) : base(connectionStringName)
        {
        }

        public SaveFeature(DbProviderFactory dbProviderFactory, string connectionString) : base(dbProviderFactory, connectionString)
        {
        }

        public void Execute(Entities.Feature feature)
        {
            using (var cnn = DbProviderFactory.CreateConnection())
            {
                if (cnn == null) return;

                cnn.ConnectionString = ConnectionString;
                cnn.Execute("INSERT INTO Feature (IsEnabled, ExpirationDays, StartDate, FeatureName, ApplicationName) " +
                            "VALUES (@IsEnabled, @ExpirationDays, @StartDate, @FeatureName, @ApplicationName)", new
                            {
                                feature.IsEnabled, feature.ExpirationDays, feature.StartDate, feature.FeatureName, feature.ApplicationName
                            });
            }
        }
    }
}