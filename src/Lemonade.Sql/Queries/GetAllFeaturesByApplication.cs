using System.Collections.Generic;
using System.Linq;
using Dapper;
using Lemonade.Data.Entities;
using Lemonade.Data.Queries;

namespace Lemonade.Sql.Queries
{
    public class GetAllFeaturesByApplication : LemonadeConnection, IGetAllFeaturesByApplication
    {
        public GetAllFeaturesByApplication()
        {
        }

        public GetAllFeaturesByApplication(string connectionStringName) : base(connectionStringName)
        {
        }

        public IList<Feature> Execute(string applicationName)
        {
            using (var cnn = CreateConnection())
            {
                var results = cnn.Query<Feature, Application, Feature>(
                    @"SELECT * FROM Feature f 
                      INNER JOIN Application a ON f.ApplicationId = a.ApplicationId
                      WHERE a.Name = @applicationName",
                    (f, a) => { f.Application = a; return f; },
                    new { applicationName },
                    splitOn: "ApplicationId");

                return results.ToList();
            }
        }
    }
}