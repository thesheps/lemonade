using System.Data.Common;
using System.Linq;
using Dapper;
using Lemonade.Core.Commands;
using Lemonade.Core.Domain;
using Lemonade.Core.Events;
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

                    DomainEvent.Raise(new FeatureHasBeenSaved(feature.FeatureId, feature.ApplicationId, feature.Name, feature.StartDate, feature.ExpirationDays, feature.IsEnabled));
                }
                catch (DbException exception)
                {
                    throw new SaveFeatureException(exception);
                }
            }
        }
    }
}