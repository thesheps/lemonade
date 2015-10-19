using System.Data.Common;
using Dapper;
using Lemonade.Data.Exceptions;
using Lemonade.Data.Queries;

namespace Lemonade.Sql.Commands
{
    public class UpdateFeatureOverride : LemonadeConnection, IUpdateFeatureOverride
    {
        public UpdateFeatureOverride()
        {
        }

        public UpdateFeatureOverride(string connectionStringName) : base(connectionStringName)
        {
        }

        public void Execute(Data.Entities.FeatureOverride featureOverride)
        {
            using (var cnn = CreateConnection())
            {
                try
                {
                    cnn.Execute(@"UPDATE FeatureOverride
                                  SET IsEnabled = @IsEnabled, Hostname = @Hostname
                                  WHERE FeatureOverrideId = @FeatureOverrideId", new
                    {
                        featureOverride.IsEnabled,
                        featureOverride.Hostname,
                        featureOverride.FeatureOverrideId
                    });
                }
                catch (DbException exception)
                {
                    throw new UpdateFeatureOverrideException(exception);
                }
            }
        }
    }
}