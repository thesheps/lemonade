using System.Data.Common;
using System.Linq;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Entities;
using Lemonade.Data.Exceptions;
using Lemonade.Sql;

namespace Lemonade.Fakes
{
    public class CreateLocaleFake : LemonadeConnection, ICreateLocale
    {
        public CreateLocaleFake()
        {
        }

        public CreateLocaleFake(string connectionStringName) : base(connectionStringName)
        {
        }

        public void Execute(Locale locale)
        {
            using (var cnn = CreateConnection())
            {
                try
                {
                    cnn.Execute("INSERT INTO Locale (IsoCode, Description) VALUES (@IsoCode, @Description)", new { locale.IsoCode, locale.Description });
                    locale.LocaleId = cnn.Query<int>("SELECT CAST(@@IDENTITY AS INT)").First();
                }
                catch (DbException exception)
                {
                    throw new CreateApplicationException(exception);
                }
            }
        }
    }
}