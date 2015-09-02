using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Lemonade.Data.Queries;

namespace Lemonade.SqlServer.Queries
{
    public class GetAllFeaturesByApplication : IGetAllFeaturesByApplication
    {
        public GetAllFeaturesByApplication(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Data.Entities.Feature> Execute(string applicationName)
        {
            using (var cnn = new SqlConnection(_connectionString))
            {
                return cnn.Query<Data.Entities.Feature>("SELECT * FROM Features WHERE Application = @applicationName", new { application = applicationName });
            }
        }

        private readonly string _connectionString;
    }
}