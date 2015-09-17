using System.Collections.Generic;
using System.Data.Common;
using Dapper;
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

        public IEnumerable<Data.Entities.Feature> Execute(string applicationName)
        {
            using (var cnn = DbProviderFactory.CreateConnection())
            {
                if (cnn == null) return null;

                cnn.ConnectionString = ConnectionString;
                return cnn.Query<Data.Entities.Feature>("SELECT * FROM Feature WHERE ApplicationName = @applicationName", new { applicationName });
            }
        }
    }
}