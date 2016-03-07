using System.Linq;
using Dapper;
using Lemonade.Data.Entities;
using Lemonade.Data.Queries;

namespace Lemonade.Sql.Queries
{
    public class GetResource : LemonadeConnection, IGetResource
    {
        public GetResource()
        {
        }

        public GetResource(string connectionStringName) : base(connectionStringName)
        {
        }

        public Resource Execute(string application, string resourceSet, string resourceKey, string locale)
        {
            using (var cnn = CreateConnection())
            {
                var resource = cnn.Query<Resource, Application, Locale, Resource>(
                    @"SELECT * FROM Resource r 
                      INNER JOIN Application a ON r.ApplicationId = a.ApplicationId
                      INNER JOIN Locale l ON r.LocaleId = l.LocaleId
                      WHERE r.ResourceSet = @resourceSet AND r.ResourceKey = @resourceKey AND
                            a.Name = @application AND l.IsoCode = @locale",
                    (r, a, l) => { r.Application = a; r.Locale = l; return r; },
                    new { resourceSet, resourceKey, application, locale },
                    splitOn: "ApplicationId,LocaleId").FirstOrDefault();

                return resource;
            }
        }
    }
}