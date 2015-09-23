using System;
using Dapper;
using Lemonade.Core.Commands;
using Lemonade.Core.Entities;
using Lemonade.Core.Exceptions;

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

        public void Execute(Feature feature)
        {
            using (var cnn = CreateConnection())
            {
                try
                {
                    cnn.Execute(
                        @"INSERT INTO Feature (IsEnabled, ExpirationDays, StartDate, Name, ApplicationId)
                          VALUES (@IsEnabled, @ExpirationDays, @StartDate, @Name, @ApplicationId)", new
                        {
                            feature.IsEnabled,
                            feature.ExpirationDays,
                            feature.StartDate,
                            feature.Name,
                            feature.ApplicationId
                        });
                }
                catch (Exception exception)
                {
                    throw new SaveFeatureException(feature.Name, exception);
                }
            }
        }
    }
}