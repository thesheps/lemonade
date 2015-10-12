using System.Collections.Generic;
using System.Linq;
using Dapper;
using Lemonade.Data.Entities;
using Lemonade.Data.Queries;

namespace Lemonade.Sql.Queries
{
    public class GetAllFeaturesByApplicationId : LemonadeConnection, IGetAllFeaturesByApplicationId
    {
        public GetAllFeaturesByApplicationId()
        {
        }

        public GetAllFeaturesByApplicationId(string connectionStringName) : base(connectionStringName)
        {
        }

        public IList<Data.Entities.Feature> Execute(int applicationId)
        {
            using (var cnn = CreateConnection())
            {
                var results = cnn.Query<Data.Entities.Feature, Application, Data.Entities.Feature>(
                    @"SELECT * FROM Feature f 
                      INNER JOIN Application a ON f.ApplicationId = a.ApplicationId
                      WHERE a.ApplicationId = @applicationId",
                    (f, a) =>
                    {
                        f.Application = a;
                        return f; 
                    },
                    new { applicationId },
                    splitOn: "ApplicationId");

                return results.ToList();
            }
        }
    }
}