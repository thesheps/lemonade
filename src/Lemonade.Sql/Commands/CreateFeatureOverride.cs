using System.Data.Common;
using System.Linq;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Entities;
using Lemonade.Data.Exceptions;

namespace Lemonade.Sql.Commands
{
    public class CreateFeatureOverride : LemonadeConnection, ICreateFeatureOverride
    {
        public CreateFeatureOverride()
        {
        }

        public CreateFeatureOverride(string connectionStringName) : base(connectionStringName)
        {
        }

        public void Execute(FeatureOverride featureOverride)
        {
            using (var cnn = CreateConnection())
            {
                try
                {
                    featureOverride.FeatureOverrideId =
                        cnn.Query<int>(@"INSERT INTO FeatureOverride (FeatureId, Hostname, IsEnabled)
                                         VALUES (@FeatureId, @Hostname, @IsEnabled);
                                         SELECT SCOPE_IDENTITY();", new
                        {
                            featureOverride.FeatureId,
                            featureOverride.Hostname,
                            featureOverride.IsEnabled
                        }).First();
                }
                catch (DbException exception)
                {
                    throw new CreateFeatureOverrideException(exception);
                }
            }
        }
    }
}