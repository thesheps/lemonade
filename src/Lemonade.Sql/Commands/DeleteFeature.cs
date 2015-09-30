using System.Data.Common;
using Dapper;
using Lemonade.Core.Commands;
using Lemonade.Core.Events;
using Lemonade.Core.Exceptions;

namespace Lemonade.Sql.Commands
{
    public class DeleteFeature : LemonadeConnection, IDeleteFeature
    {
        public DeleteFeature()
        {
        }

        public DeleteFeature(string connectionStringName) : base(connectionStringName)
        {
        }

        public void Execute(int featureId)
        {
            using (var cnn = CreateConnection())
            {
                cnn.Query("DELETE FROM Feature WHERE FeatureId = @featureId", new { featureId });
                DomainEvent.Raise(new FeatureHasBeenDeleted(featureId));
            }
        }
    }
}