using System.Configuration;
using System.Data.Common;
using System.Linq;
using Dapper;
using Lemonade.Sql.Exceptions;

namespace Lemonade.Sql.Queries
{
    public class GetFeatureByNameAndApplication
    {
        public GetFeatureByNameAndApplication() 
            : this("Lemonade")
        {
        }

        public GetFeatureByNameAndApplication(string connectionStringName)
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (connectionStringSettings == null)
                throw new ConnectionStringNotFoundException(connectionStringName);

            _dbProviderFactory = DbProviderFactories.GetFactory(connectionStringSettings.ProviderName);
            _connectionString = connectionStringSettings.ConnectionString;
        }

        public GetFeatureByNameAndApplication(DbProviderFactory dbProviderFactory, string connectionString)
        {
            _dbProviderFactory = dbProviderFactory;
            _connectionString = connectionString;
        }

        public Entities.Feature Execute(string featureName, string applicationName)
        {
            using (var cnn = _dbProviderFactory.CreateConnection())
            {
                if (cnn == null) return null;

                cnn.ConnectionString = _connectionString;

                return cnn.Query<Entities.Feature>("SELECT * FROM Feature WHERE FeatureName = @featureName " +
                                                   "AND ApplicationName = @applicationName",
                    new
                    {
                        featureName,
                        applicationName
                    }).SingleOrDefault();
            }
        }

        private readonly DbProviderFactory _dbProviderFactory;
        private readonly string _connectionString;
    }
}