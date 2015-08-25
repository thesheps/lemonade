using System.Data.Common;
using System.Linq;
using Dapper;

namespace Lemonade.SqlServer.Queries
{
    public class GetFeatureByName
    {
        public Entities.Feature Execute(string value)
        {
            using (var cnn = DbProviderFactories.GetFactory("Lemonade").CreateConnection())
            {
                return cnn.Query<Entities.Feature>("SELECT * FROM Features WHERE Name = @name", new { name = value }).First();
            }
        }
    }
}