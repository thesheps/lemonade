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
                var resources = cnn.Query<Resource, Application, Locale, Resource>(
                    @"SELECT * FROM Resource r
                      INNER JOIN Application a ON r.ApplicationId = a.ApplicationId
                      INNER JOIN Locale l ON r.LocaleId = l.LocaleId
                      WHERE a.ApplicationId = @applicationId",
                    (r, a, l) =>
                    {
                        r.Locale = l;
                        r.Application = a;
                        return r;
                    },
                    new { applicationId },
                    splitOn: "ApplicationId,LocaleId").ToList();

                return resources.ToList();
            }
        }
    }
}