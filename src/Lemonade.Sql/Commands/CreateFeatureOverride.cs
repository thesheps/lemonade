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
                    cnn.Execute(@"INSERT INTO FeatureOverride (FeatureId, Hostname, IsEnabled)
                                  VALUES (@FeatureId, @Hostname, @IsEnabled)", new
                    {
                        featureOverride.FeatureId,
                        featureOverride.Hostname,
                        featureOverride.IsEnabled
                    });

                    featureOverride.FeatureOverrideId = cnn.Query<int>("SELECT CAST(@@IDENTITY AS INT)").First();
                }
                catch (DbException exception)
                {
                    throw new CreateFeatureException(exception);
                }
            }
        }
    }
}