using System.Collections.Generic;
using System.Data.Common;
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

        public GetAllFeaturesByApplication(DbProviderFactory dbProviderFactory, string connectionString) : base(dbProviderFactory, connectionString)
        {
        }

        public IList<Feature> Execute(string applicationName)
        {
            using (var cnn = DbProviderFactory.CreateConnection())
            {
                if (cnn == null) return null;

                cnn.ConnectionString = ConnectionString;

                var results = cnn.Query<Feature, Application, Feature>(
                    @"SELECT * FROM Feature f INNER JOIN Application a ON f.ApplicationId = a.ApplicationId
                      WHERE a.Name = @applicationName",
                    (f, a) => { f.Application = a; return f; },
                    new { applicationName },
                    splitOn: "ApplicationId");

                return results.ToList();
            }
        }
    }
}