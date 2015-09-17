using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Dapper;
using Lemonade.Data.Entities;
using Lemonade.Data.Queries;

namespace Lemonade.Sql.Queries
{
    public class GetAllApplications : LemonadeConnection, IGetAllApplications
    {
        public GetAllApplications()
        {
        }

        public GetAllApplications(string connectionStringName) : base(connectionStringName)
        {
        }

        public GetAllApplications(DbProviderFactory dbProviderFactory, string connectionString) : base(dbProviderFactory, connectionString)
        {
        }

        public IList<Application> Execute()
        {
            using (var cnn = DbProviderFactory.CreateConnection())
            {
                if (cnn == null) return null;

                cnn.ConnectionString = ConnectionString;
                return cnn.Query<Application>("SELECT * FROM Application").ToList();
            }
        }
    }
}