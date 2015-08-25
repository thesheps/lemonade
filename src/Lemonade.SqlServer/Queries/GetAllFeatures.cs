using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Lemonade.Data.Queries;

namespace Lemonade.SqlServer.Queries
{
    public class GetAllFeatures : IGetAllFeatures
    {
        public GetAllFeatures(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Data.Entities.Feature> Execute()
        {
            using (var cnn = new SqlConnection(_connectionString))
            {
                return cnn.Query<Data.Entities.Feature>("SELECT * FROM Features");
            }
        }

        private readonly string _connectionString;
    }
}