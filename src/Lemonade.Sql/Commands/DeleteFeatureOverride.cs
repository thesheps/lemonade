using System.Data.Common;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;

namespace Lemonade.Sql.Commands
{
    public class DeleteFeatureOverride : LemonadeConnection, IDeleteFeatureOverride
    {
        public DeleteFeatureOverride()
        {
        }

        public DeleteFeatureOverride(string connectionStringName) : base(connectionStringName)
        {
        }

        public void Execute(int featureOverrideId)
        {
            using (var cnn = CreateConnection())
            {
                try
                {
                    cnn.Query("DELETE FROM FeatureOverride WHERE FeatureOverrideId = @featureOverrideId", new { featureOverrideId });
                }
                catch (DbException exception)
                {
                    throw new DeleteFeatureOverrideException(exception);
                }
            }
        }
    }
}