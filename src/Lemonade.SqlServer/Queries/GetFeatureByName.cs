using System;
using System.Data.Common;
using System.Linq;
using Dapper;
using Lemonade.Data.Queries;

namespace Lemonade.Sql.Queries
{
    public class GetFeatureByName : IGetFeatureByName
    {
        public GetFeatureByName(DbProviderFactory dbProviderFactory, string connectionString)
        {
            if (dbProviderFactory == null) throw new ArgumentNullException(nameof(dbProviderFactory));
            _dbProviderFactory = dbProviderFactory;
            _connectionString = connectionString;
        }

        public Data.Entities.Feature Execute(string name)
        {
            using (var cnn = _dbProviderFactory.CreateConnection())
            {
                if (cnn == null) return null;

                cnn.ConnectionString = _connectionString;
                return cnn.Query<Data.Entities.Feature>("SELECT * FROM Features WHERE Name = @name", new { name = name }).First();
            }
        }

        private readonly DbProviderFactory _dbProviderFactory;
        private readonly string _connectionString;
    }
}