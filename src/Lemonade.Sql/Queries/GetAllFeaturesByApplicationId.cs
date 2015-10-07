using System.Collections.Generic;
using System.Linq;
using Dapper;
using Lemonade.Core.Domain;
using Lemonade.Core.Queries;

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

        public IList<Core.Domain.Feature> Execute(int applicationId)
        {
            using (var cnn = CreateConnection())
            {
                var results = cnn.Query<Core.Domain.Feature, Application, Core.Domain.Feature>(
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