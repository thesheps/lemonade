using System.Collections.Generic;
using System.Linq;
using Dapper;
using Lemonade.Data.Entities;
using Lemonade.Data.Queries;

namespace Lemonade.Sql.Queries
{
    public class GetAllLocales : LemonadeConnection, IGetAllLocales
    {
        public GetAllLocales()
        {
        }

        public GetAllLocales(string connectionStringName) : base(connectionStringName)
        {
        }

        public IList<Locale> Execute()
        {
            using (var cnn = CreateConnection())
            {
                return cnn.Query<Locale>("SELECT * FROM Locale").ToList();
            }
        }
    }
}