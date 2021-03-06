using System.Data.Common;
using System.Linq;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Entities;
using Lemonade.Data.Exceptions;

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
                    application.ApplicationId = cnn.Query<int>(@"INSERT INTO Application (Name) VALUES (@Name);
                                                                 SELECT SCOPE_IDENTITY();", new { application.Name }).First();
                }
                catch (DbException exception)
                {
                    throw new CreateApplicationException(exception);
                }
            }
        }
    }
}