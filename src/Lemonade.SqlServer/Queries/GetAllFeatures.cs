using System.Collections.Generic;
using System.Data.Common;
using Dapper;
using Lemonade.Data.Queries;

namespace Lemonade.Sql.Queries
{
    public class GetAllFeatures : IGetAllFeatures
    {
        public GetAllFeatures(DbProviderFactory dbProviderFactory, string connectionString)
        {
            _dbProviderFactory = dbProviderFactory;
            _connectionString = connectionString;
        }

        public IEnumerable<Data.Entities.Feature> Execute()
        {
            using (var cnn = _dbProviderFactory.CreateConnection())
            {
                if (cnn == null) return null;

                cnn.ConnectionString = _connectionString;
                return cnn.Query<Data.Entities.Feature>("SELECT * FROM Feature");
            }
        }

        private readonly DbProviderFactory _dbProviderFactory;
        private readonly string _connectionString;
    }
}