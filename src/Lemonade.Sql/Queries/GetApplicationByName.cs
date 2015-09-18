using System.Data.Common;
using System.Linq;
using Dapper;
using Lemonade.Data.Entities;
using Lemonade.Data.Queries;

namespace Lemonade.Sql.Queries
{
    public class GetApplicationByName : LemonadeConnection, IGetApplicationByName
    {
        public GetApplicationByName()
        {
        }

        public GetApplicationByName(string connectionStringName) : base(connectionStringName)
        {
        }

        public GetApplicationByName(DbProviderFactory dbProviderFactory, string connectionString) : base(dbProviderFactory, connectionString)
        {
        }

        public Application Execute(string applicationName)
        {
            using (var cnn = DbProviderFactory.CreateConnection())
            {
                if (cnn == null) return null;

                cnn.ConnectionString = ConnectionString;

                return cnn.Query<Application>("SELECT * FROM Application a WHERE a.Name = @applicationName",new { applicationName }).SingleOrDefault();
            }
        }
    }
}