using System.Data.Common;
using System.Linq;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Entities;
using Lemonade.Data.Exceptions;

namespace Lemonade.Sql.Commands
{
    public class CreateLocale : LemonadeConnection, ICreateLocale
    {
        public CreateLocale()
        {
        }

        public CreateLocale(string connectionStringName) : base(connectionStringName)
        {
        }

        public void Execute(Locale locale)
        {
            using (var cnn = CreateConnection())
            {
                try
                {
                    locale.LocaleId = cnn.Query<int>(@"INSERT INTO Locale (IsoCode, Description) VALUES (@IsoCode, @Description);
                                                       SELECT SCOPE_IDENTITY();", new { locale.IsoCode, locale.Description }).First();
                }
                catch (DbException exception)
                {
                    throw new CreateApplicationException(exception);
                }
            }
        }
    }
}