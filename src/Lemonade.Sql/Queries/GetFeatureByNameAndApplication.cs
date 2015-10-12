using System.Linq;
using Dapper;
using Lemonade.Data.Entities;
using Lemonade.Data.Queries;

namespace Lemonade.Sql.Queries
{
    public class GetFeatureByNameAndApplication : LemonadeConnection, IGetFeatureByNameAndApplication
    {
        public GetFeatureByNameAndApplication()
        {
        }

        public GetFeatureByNameAndApplication(string connectionStringName) : base(connectionStringName)
        {
        }

        public Data.Entities.Feature Execute(string featureName, string applicationName)
        {
            using (var cnn = CreateConnection())
            {
                var results = cnn.Query<Data.Entities.Feature, Application, Data.Entities.Feature>(
                    @"SELECT * FROM Feature f INNER JOIN Application a ON f.ApplicationId = a.ApplicationId
                      WHERE f.Name = @featureName AND a.Name = @applicationName",
                    (f, a) => { f.Application = a; return f; },
                    new { featureName, applicationName },
                    splitOn: "ApplicationId").FirstOrDefault();

                return results;
            }
        }
    }
}