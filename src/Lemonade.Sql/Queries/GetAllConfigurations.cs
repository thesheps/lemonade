using System.Collections.Generic;
using System.Linq;
using Dapper;
using Lemonade.Data.Entities;
using Lemonade.Data.Queries;

namespace Lemonade.Sql.Queries
{
    public class GetAllConfigurations : LemonadeConnection, IGetAllConfigurations
    {
        public GetAllConfigurations()
        {
        }

        public GetAllConfigurations(string connectionStringName) : base(connectionStringName)
        {
        }

        public IList<Data.Entities.Configuration> Execute()
        {
            using (var cnn = CreateConnection())
            {
                var results = cnn.Query<Data.Entities.Configuration, Application, Data.Entities.Configuration>(
                    @"SELECT * FROM Configuration c INNER JOIN Application a ON c.ApplicationId = a.ApplicationId",
                    (c, a) =>
                    {
                        c.Application = a;
                        return c;
                    },
                    splitOn: "ApplicationId");

                return results.ToList();
            }
        }
    }
}