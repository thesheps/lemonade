using System.Collections.Generic;
using System.Linq;
using Dapper;
using Lemonade.Data.Entities;
using Lemonade.Data.Queries;

namespace Lemonade.Sql.Queries
{
    public class GetAllResourcesByApplicationId : LemonadeConnection, IGetAllResourcesByApplicationId
    {
        public GetAllResourcesByApplicationId()
        {
        }

        public GetAllResourcesByApplicationId(string connectionStringName) : base(connectionStringName)
        {
        }

        public IList<Resource> Execute(int applicationId)
        {
            using (var cnn = CreateConnection())
            {
                var resources = cnn.Query<Resource, Application, Resource>(
                    @"SELECT * FROM Resource r
                      INNER JOIN Application a ON r.ApplicationId = a.ApplicationId
                      WHERE a.ApplicationId = @applicationId",
                    (r, a) =>
                    {
                        r.Application = a;
                        return r;
                    },
                    new { applicationId },
                    splitOn: "ApplicationId").ToList();

                return resources.ToList();
            }
        }
    }
}