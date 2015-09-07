using System.Configuration;
using System.Data.Common;
using System.Linq;
using Dapper;
using Lemonade.Sql.Exceptions;

namespace Lemonade.Sql.Queries
{
    public class GetFeatureByName
    {
        public GetFeatureByName() 
            : this("Lemonade")
        {
        }

        public GetFeatureByName(string connectionStringName)
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (connectionStringSettings == null)
                throw new ConnectionStringNotFoundException(connectionStringName);

            _dbProviderFactory = DbProviderFactories.GetFactory(connectionStringSettings.ProviderName);
            _connectionString = connectionStringSettings.ConnectionString;
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
                return cnn.Query<Entities.Feature>("SELECT * FROM Feature WHERE Name = @name", new { name }).First();
            }
        }

        private readonly DbProviderFactory _dbProviderFactory;
        private readonly string _connectionString;
    }
}