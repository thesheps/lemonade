using System.Data.Common;
using Dapper;
using Lemonade.Core.Commands;
using Lemonade.Core.Domain;
using Lemonade.Core.Events;
using Lemonade.Core.Exceptions;

namespace Lemonade.Sql.Commands
{
    public class UpdateFeature : LemonadeConnection, IUpdateFeature
    {
        public UpdateFeature()
        {
        }

        public UpdateFeature(string connectionStringName) : base(connectionStringName)
        {
        }

        public void Execute(Feature feature)
        {
            using (var cnn = CreateConnection())
            {
                try
                {
                    cnn.Execute(@"UPDATE Feature
                                  SET IsEnabled = @IsEnabled, ExpirationDays = @ExpirationDays, StartDate = @StartDate, Name = @Name
                                  WHERE FeatureId = @FeatureId", new
                    {
                        feature.IsEnabled,
                        feature.ExpirationDays,
                        feature.StartDate,
                        feature.Name,
                        feature.FeatureId
                    });

                    DomainEvent.Raise(new FeatureHasBeenUpdated(feature.FeatureId, feature.ApplicationId, feature.Name, feature.StartDate, feature.ExpirationDays, feature.IsEnabled));
                }
                catch (DbException exception)
                {
                    throw new UpdateFeatureException(exception);
                }
            }
        }
    }
}