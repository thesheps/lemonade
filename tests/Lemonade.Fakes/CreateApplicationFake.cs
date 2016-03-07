using System.Data.Common;
using System.Linq;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Entities;
using Lemonade.Data.Exceptions;
using Lemonade.Sql;

namespace Lemonade.Fakes
{
    public class CreateApplicationFake : LemonadeConnection, ICreateApplication
    {
        public CreateApplicationFake()
        {
        }

        public CreateApplicationFake(string connectionStringName) : base(connectionStringName)
        {
        }

        public void Execute(Application application)
        {
            using (var cnn = CreateConnection())
            {
                try
                {
                    cnn.Execute("INSERT INTO Application (Name) VALUES (@Name)", new { application.Name });
                    application.ApplicationId = cnn.Query<int>("SELECT CAST(@@IDENTITY AS INT)").First();
                }
                catch (DbException exception)
                {
                    throw new CreateApplicationException(exception);
                }
            }
        }
    }
}