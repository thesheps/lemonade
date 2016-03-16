using System.Collections.Generic;
using System.Linq;
using Dapper;
using Lemonade.Data.Entities;
using Lemonade.Data.Queries;

namespace Lemonade.Sql.Queries
{
    public class GetAllResourcesByApplicationIdAndLocaleId : LemonadeConnection, IGetAllResourcesByApplicationIdAndLocaleId
    {
        public GetAllResourcesByApplicationIdAndLocaleId()
        {
        }

        public GetAllResourcesByApplicationIdAndLocaleId(string connectionStringName) : base(connectionStringName)
        {
        }

        public IList<Resource> Execute(int applicationId, int localeId)
        {
            using (var cnn = CreateConnection())
            {
                var resources = cnn.Query<Resource, Application, Locale, Resource>(
                    @"SELECT * FROM Resource r
                      INNER JOIN Application a ON r.ApplicationId = a.ApplicationId
                      INNER JOIN Locale l ON r.LocaleId = l.LocaleId
                      WHERE a.ApplicationId = @applicationId AND l.LocaleId = @localeId",
                    (r, a, l) =>
                    {
                        r.Locale = l;
                        r.Application = a;
                        return r;
                    },
                    new { applicationId, localeId },
                    splitOn: "ApplicationId,LocaleId").ToList();

                return resources.ToList();
            }
        }
    }
}