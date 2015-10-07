using System.Data.Common;
using System.Linq;
using Dapper;
using Lemonade.Core.Commands;
using Lemonade.Core.Exceptions;

namespace Lemonade.Sql.Commands
{
    public class CreateFeature : LemonadeConnection, ICreateFeature
    {
        public CreateFeature()
        {
        }

        public CreateFeature(string connectionStringName) : base(connectionStringName)
        {
        }

        public void Execute(Core.Domain.Feature feature)
        {
            using (var cnn = CreateConnection())
            {
                try
                {
                    cnn.Execute(@"INSERT INTO Feature (IsEnabled, ExpirationDays, StartDate, Name, ApplicationId)
                                  VALUES (@IsEnabled, @ExpirationDays, @StartDate, @Name, @ApplicationId)", new
                        {
                            feature.IsEnabled,
                            feature.ExpirationDays,
                            feature.StartDate,
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