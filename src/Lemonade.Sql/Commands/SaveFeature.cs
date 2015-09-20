using System;
using System.Data.Common;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Entities;
using Lemonade.Data.Exceptions;
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