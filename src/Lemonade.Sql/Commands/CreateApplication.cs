using System.Data.Common;
using System.Linq;
using Dapper;
using Lemonade.Core.Commands;
using Lemonade.Core.Domain;
using Lemonade.Core.Exceptions;

namespace Lemonade.Sql.Commands
{
    public class CreateApplication : LemonadeConnection, ICreateApplication
    {
        public CreateApplication()
        {
        }

        public CreateApplication(string connectionStringName) : base(connectionStringName)
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