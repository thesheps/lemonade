using System.Data.Common;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;

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

        public void Execute(Data.Entities.Feature feature)
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
                }
                catch (DbException exception)
                {
                    throw new UpdateFeatureException(exception);
                }
            }
        }
    }
}