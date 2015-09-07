using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using Dapper;

namespace Lemonade.Sql.Queries
{
    public class GetAllFeaturesByApplication
    {
        public GetAllFeaturesByApplication() 
            : this("Lemonade")
        {
        }

        public GetAllFeaturesByApplication(string connectionStringName) 
            : this(ConfigurationManager.ConnectionStrings[connectionStringName])
        {
        }

        public GetAllFeaturesByApplication(ConnectionStringSettings connectionStringSettings) 
            : this(DbProviderFactories.GetFactory(connectionStringSettings.ProviderName), connectionStringSettings.ConnectionString)
        {
        }

        public GetAllFeaturesByApplication(DbProviderFactory dbProviderFactory, string connectionString)
        {
            _dbProviderFactory = dbProviderFactory;
            _connectionString = connectionString;
        }

        public IEnumerable<Feature> Execute(string applicationName)
        {
            using (var cnn = _dbProviderFactory.CreateConnection())
            {
                if (cnn == null) return null;

                cnn.ConnectionString = _connectionString;
                return cnn.Query<Feature>("SELECT * FROM Features WHERE Application = @applicationName", new { application = applicationName });
            }
        }

        private readonly DbProviderFactory _dbProviderFactory;
        private readonly string _connectionString;
    }
}