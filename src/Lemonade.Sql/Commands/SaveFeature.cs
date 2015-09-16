using System;
using System.Data.Common;
using System.Data.SqlClient;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Sql.Exceptions;

namespace Lemonade.Sql.Commands
{
    public class SaveFeature : LemonadeConnection, ISaveFeature
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

        public void Execute(Data.Entities.Feature feature)
        {
            using (var cnn = DbProviderFactory.CreateConnection())
            {
                if (cnn == null) return;

                cnn.ConnectionString = ConnectionString;

                try
                {
                    cnn.Execute(
                        "INSERT INTO Feature (IsEnabled, ExpirationDays, StartDate, FeatureName, ApplicationName) " +
                        "VALUES (@IsEnabled, @ExpirationDays, @StartDate, @FeatureName, @ApplicationName)", new
                        {
                            feature.IsEnabled,
                            feature.ExpirationDays,
                            feature.StartDate,
                            feature.FeatureName,
                            feature.ApplicationName
                        });
                }
                catch (DbException exception)
                {
                    throw new SaveFeatureException(feature.ApplicationName, feature.FeatureName, exception);
                }
            }
        }
    }
}