using System.Data.Common;
using System.Linq;
using Dapper;
using Lemonade.Data.Queries;

namespace Lemonade.SqlServer.Queries
{
    public class GetFeatureByName : IGetFeatureByName
    {
        public Data.Entities.Feature Execute(string value)
        {
            using (var cnn = DbProviderFactories.GetFactory("Lemonade").CreateConnection())
            {
                return cnn.Query<Data.Entities.Feature>("SELECT * FROM Features WHERE Name = @name", new { name = value }).First();
            }
        }
    }
}