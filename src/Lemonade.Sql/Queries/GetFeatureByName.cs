using System;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using Dapper;

namespace Lemonade.Sql.Queries
{
    public class GetFeatureByName
    {
        public GetFeatureByName()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Lemonade"];
            _dbProviderFactory = DbProviderFactories.GetFactory(connectionString.ProviderName);
            _connectionString = connectionString.ConnectionString;
        }

        public GetFeatureByName(DbProviderFactory dbProviderFactory, string connectionString)
        {
            if (dbProviderFactory == null) throw new ArgumentNullException(nameof(dbProviderFactory));
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