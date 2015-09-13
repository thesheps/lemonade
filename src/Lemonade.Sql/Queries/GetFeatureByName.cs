using System.Data.Common;
using System.Linq;
using Dapper;
using Lemonade.Data.Queries;

namespace Lemonade.Sql.Queries
{
    public class GetFeatureByName : LemonadeConnection, IGetFeatureByName
    {
        public GetFeatureByName()
        {
        }

        public GetFeatureByName(string connectionStringName) : base(connectionStringName)
        {
        }

        public GetFeatureByName(DbProviderFactory dbProviderFactory, string connectionString) : base(dbProviderFactory, connectionString)
        {
        }

        public Data.Entities.Feature Execute(string featureName)
        {
            using (var cnn = DbProviderFactory.CreateConnection())
            {
                if (cnn == null) return null;

                cnn.ConnectionString = ConnectionString;
                return cnn.Query<Data.Entities.Feature>("SELECT * FROM Feature WHERE FeatureName = @featureName", new { featureName }).First();
            }
        }
    }
}