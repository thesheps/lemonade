using System.Data.Common;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;

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
                try
                {
                    cnn.Query("DELETE FROM Feature WHERE FeatureId = @featureId", new { featureId });
                }
                catch (DbException exception)
                {
                    throw new DeleteFeatureException(exception);
                }
            }
        }
    }
}