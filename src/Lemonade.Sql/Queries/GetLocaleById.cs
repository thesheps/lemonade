using System.Linq;
using Dapper;
using Lemonade.Data.Entities;
using Lemonade.Data.Queries;

namespace Lemonade.Sql.Queries
{
    public class GetLocaleById : LemonadeConnection, IGetLocaleById
    {
        public GetLocaleById()
        {
        }

        public GetLocaleById(string connectionStringName) : base(connectionStringName)
        {
        }

        public Locale Execute(int localeId)
        {
            using (var cnn = CreateConnection())
            {
                return cnn.Query<Locale>("SELECT * FROM Locale WHERE localeId = @localeId", new { localeId }).First();
            }
        }
    }
}