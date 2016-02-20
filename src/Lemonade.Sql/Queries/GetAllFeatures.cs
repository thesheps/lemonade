using System.Collections.Generic;
using System.Linq;
using Dapper;
using Lemonade.Data.Entities;
using Lemonade.Data.Queries;

namespace Lemonade.Sql.Queries
{
    public class GetAllFeatures : LemonadeConnection, IGetAllFeatures
    {
        public GetAllFeatures()
        {
        }

        public GetAllFeatures(string connectionStringName) : base(connectionStringName)
        {
        }

        public IList<Data.Entities.Feature> Execute()
        {
            using (var cnn = CreateConnection())
            {
                var results = cnn.Query<Data.Entities.Feature, Application, Data.Entities.Feature>(
                    @"SELECT * FROM Feature f INNER JOIN Application a ON f.ApplicationId = a.ApplicationId",
                    (f, a) => 
                    {
                        f.Application = a;
                        return f;
                    },
                    splitOn: "ApplicationId");

                return results.ToList();
            }
        }
    }
}