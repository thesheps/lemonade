using System.Data.Common;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Entities;
using Lemonade.Data.Exceptions;

namespace Lemonade.Sql.Commands
{
    public class UpdateApplication : LemonadeConnection, IUpdateApplication
    {
        public UpdateApplication()
        {
        }

        public UpdateApplication(string connectionStringName) : base(connectionStringName)
        {
        }

        public void Execute(Application application)
        {
            using (var cnn = CreateConnection())
            {
                try
                {
                    cnn.Execute(@"UPDATE Application SET Name = @Name WHERE ApplicationId = @ApplicationId", new
                    {
                        application.Name,
                        application.ApplicationId
                    });
                }
                catch (DbException exception)
                {
                    throw new UpdateApplicationException(exception);
                }
            }
        }
    }
}