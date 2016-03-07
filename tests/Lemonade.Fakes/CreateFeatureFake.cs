using System.Data.Common;
using System.Linq;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;
using Lemonade.Sql;

namespace Lemonade.Fakes
{
    public class CreateFeatureFake : LemonadeConnection, ICreateFeature
    {
        public CreateFeatureFake()
        {
        }

        public CreateFeatureFake(string connectionStringName) : base(connectionStringName)
        {
        }

        public void Execute(Data.Entities.Feature feature)
        {
            using (var cnn = CreateConnection())
            {
                try
                {
                    cnn.Execute(@"INSERT INTO Feature (IsEnabled, Name, ApplicationId)
                                  VALUES (@IsEnabled, @Name, @ApplicationId)", new
                    {
                        feature.IsEnabled,
                        feature.Name,
                        feature.ApplicationId
                    });

                    feature.FeatureId = cnn.Query<int>("SELECT CAST(@@IDENTITY AS INT)").First();
                }
                catch (DbException exception)
                {
                    throw new CreateFeatureException(exception);
                }
            }
        }
    }
}