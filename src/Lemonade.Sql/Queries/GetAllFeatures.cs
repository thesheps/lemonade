using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using Dapper;

namespace Lemonade.Sql.Queries
{
    public class GetAllFeatures
    {
        public GetAllFeatures()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Lemonade"];
            _dbProviderFactory = DbProviderFactories.GetFactory(connectionString.ProviderName);
            _connectionString = connectionString.ConnectionString;
        }

        public GetAllFeatures(DbProviderFactory dbProviderFactory, string connectionString)
        {
            _dbProviderFactory = dbProviderFactory;
            _connectionString = connectionString;
        }

        public IEnumerable<Entities.Feature> Execute()
        {
            using (var cnn = _dbProviderFactory.CreateConnection())
            {
                if (cnn == null) return null;

                cnn.ConnectionString = _connectionString;
                return cnn.Query<Entities.Feature>("SELECT * FROM Feature");
            }
        }

        private readonly DbProviderFactory _dbProviderFactory;
        private readonly string _connectionString;
    }
}