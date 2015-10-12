using System.Collections.Generic;
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

        public IList<Application> Execute()
        {
            using (var cnn = CreateConnection())
            {
                return cnn.Query<Application>("SELECT * FROM Application").ToList();
            }
        }
    }
}