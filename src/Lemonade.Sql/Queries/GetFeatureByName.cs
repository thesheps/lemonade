using System.Configuration;
using System.Data.Common;
using System.Linq;
using Dapper;

namespace Lemonade.Sql.Queries
{
    public class GetFeatureByName
    {
        public GetFeatureByName() 
            : this("Lemonade")
        {
        }

        public GetFeatureByName(string connectionStringName) 
            : this(ConfigurationManager.ConnectionStrings[connectionStringName])
        {
        }

        public GetFeatureByName(ConnectionStringSettings connectionStringSettings) 
            : this(DbProviderFactories.GetFactory(connectionStringSettings.ProviderName), connectionStringSettings.ConnectionString)
        {
        }

        public GetFeatureByName(DbProviderFactory dbProviderFactory, string connectionString)
        {
            _dbProviderFactory = dbProviderFactory;
            _connectionString = connectionString;
        }

        public Entities.Feature Execute(string name)
        {
            using (var cnn = _dbProviderFactory.CreateConnection())
            {
                if (cnn == null) return null;

                cnn.ConnectionString = _connectionString;
                return cnn.Query<Entities.Feature>("SELECT * FROM Features WHERE Name = @name", new { name }).First();
            }
        }

        private readonly DbProviderFactory _dbProviderFactory;
        private readonly string _connectionString;
    }
}