using System.Collections.Generic;
using System.Linq;
using Dapper;
using Lemonade.Core.Domain;
using Lemonade.Core.Queries;

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

        public IList<Core.Domain.Feature> Execute()
        {
            using (var cnn = CreateConnection())
            {
                var results = cnn.Query<Core.Domain.Feature, Application, Core.Domain.Feature>(
                    @"SELECT * FROM Feature f INNER JOIN Application a ON f.ApplicationId = a.ApplicationId
                      WHERE a.Name = @applicationName",
                    (f, a) => 
                    {
                        f.Application = a;
                        return f;
                    },
                    splitOn: "FeatureId");

                return results.ToList();
            }
        }
    }
}