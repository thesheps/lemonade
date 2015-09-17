using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Dapper;
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

        public GetAllFeatures(DbProviderFactory dbProviderFactory, string connectionString) : base(dbProviderFactory, connectionString)
        {
        }

        public IList<Data.Entities.Feature> Execute()
        {
            using (var cnn = DbProviderFactory.CreateConnection())
            {
                if (cnn == null) return null;

                cnn.ConnectionString = ConnectionString;
                return cnn.Query<Data.Entities.Feature>("SELECT * FROM Feature").ToList();
            }
        }
    }
}