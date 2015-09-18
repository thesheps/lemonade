using System.Data.Common;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Entities;
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

        public void Execute(Feature feature)
        {
            using (var cnn = DbProviderFactory.CreateConnection())
            {
                if (cnn == null) return;

                cnn.ConnectionString = ConnectionString;

                try
                {
                    cnn.Execute(
                        "INSERT INTO Feature (IsEnabled, ExpirationDays, StartDate, Name, ApplicationId) " +
                        "VALUES (@IsEnabled, @ExpirationDays, @StartDate, @Name, @ApplicationId)", new
                        {
                            feature.IsEnabled,
                            feature.ExpirationDays,
                            feature.StartDate,
                            feature.Name,
                            feature.Application.ApplicationId
                        });
                }
                catch (DbException exception)
                {
                    throw new SaveFeatureException(feature.Application.Name, feature.Name, exception);
                }
            }
        }
    }
}