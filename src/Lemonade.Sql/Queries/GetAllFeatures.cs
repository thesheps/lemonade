using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using Dapper;

namespace Lemonade.Sql.Queries
{
    public class GetAllFeatures
    {
        public GetAllFeatures() 
            : this("Lemonade")
        {
        }

        public GetAllFeatures(string connectionStringName) 
            : this(ConfigurationManager.ConnectionStrings[connectionStringName])
        {
        }

        public GetAllFeatures(ConnectionStringSettings connectionStringSettings) 
            : this(DbProviderFactories.GetFactory(connectionStringSettings.ProviderName), connectionStringSettings.ConnectionString)
        {
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