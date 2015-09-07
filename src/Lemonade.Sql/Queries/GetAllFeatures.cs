using System.Collections.Generic;
using System.Data.Common;
using Dapper;

namespace Lemonade.Sql.Queries
{
    public class GetAllFeatures : LemonadeConnection
    {
        public GetAllFeatures()
        {
        }

        public GetAllFeatures(string connectionStringName) : base(connectionStringName)
        {
        }

        public GetAllFeatures(DbProviderFactory dbProviderFactory, string connectionString) : base(dbProviderFactory, connectionString)
        {
        }

        public IEnumerable<Entities.Feature> Execute()
        {
            using (var cnn = DbProviderFactory.CreateConnection())
            {
                if (cnn == null) return null;

                cnn.ConnectionString = ConnectionString;
                return cnn.Query<Entities.Feature>("SELECT * FROM Feature");
            }
        }
    }
}