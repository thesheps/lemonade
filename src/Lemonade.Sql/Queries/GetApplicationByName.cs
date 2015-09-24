using System.Linq;
using Dapper;
using Lemonade.Core.Domain;
using Lemonade.Core.Queries;

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

        public Application Execute(string applicationName)
        {
            using (var cnn = CreateConnection())
            {
                return cnn.Query<Application>("SELECT * FROM Application a WHERE a.Name = @applicationName", 
                    new { applicationName }).SingleOrDefault();
            }
        }
    }
}