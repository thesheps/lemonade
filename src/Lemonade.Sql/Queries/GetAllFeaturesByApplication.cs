using System.Collections.Generic;
using System.Data.Common;
using Dapper;

namespace Lemonade.Sql.Queries
{
    public class GetAllFeaturesByApplication : LemonadeConnection
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

        public IEnumerable<Feature> Execute(string applicationName)
        {
            using (var cnn = DbProviderFactory.CreateConnection())
            {
                if (cnn == null) return null;

                cnn.ConnectionString = ConnectionString;
                return cnn.Query<Feature>("SELECT * FROM Features WHERE ApplicationName = @applicationName", new { application = applicationName });
            }
        }
    }
}