using System.Data.Common;
using System.Linq;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Entities;
using Lemonade.Data.Exceptions;
using Lemonade.Sql;

namespace Lemonade.Fakes
{
    public class CreateFeatureOverrideFake : LemonadeConnection, ICreateFeatureOverride
    {
        public CreateFeatureOverrideFake()
        {
        }

        public CreateFeatureOverrideFake(string connectionStringName) : base(connectionStringName)
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
                    throw new CreateFeatureOverrideException(exception);
                }
            }
        }
    }
}