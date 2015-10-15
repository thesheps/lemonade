using System.Linq;
using Dapper;
using Lemonade.Data.Entities;
using Lemonade.Data.Queries;

namespace Lemonade.Sql.Queries
{
    public class GetFeatureOverride : LemonadeConnection, IGetFeatureOverride
    {
        public GetFeatureOverride()
        {
        }

        public GetFeatureOverride(string connectionStringName) : base(connectionStringName)
        {
        }

        public FeatureOverride Execute(int featureId, string hostname)
        {
            using (var cnn = CreateConnection())
            {
                return cnn.Query<FeatureOverride>(@"SELECT * FROM FeatureOverride f WHERE f.FeatureId = @featureId AND f.Hostname = @hostname",
                                                    new { featureId, hostname }).SingleOrDefault();
            }
        }
    }
}