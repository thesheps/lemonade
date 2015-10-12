using System.Data.Common;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;

namespace Lemonade.Sql.Commands
{
    public class DeleteApplication : LemonadeConnection, IDeleteApplication
    {
        public DeleteApplication()
        {
        }

        public DeleteApplication(string connectionStringName) : base(connectionStringName)
        {
        }

        public void Execute(int applicationId)
        {
            using (var cnn = CreateConnection())
            {
                try
                {
                    cnn.Query("DELETE FROM Application WHERE ApplicationId = @applicationId", new {applicationId});
                }
                catch (DbException exception)
                {
                    throw new DeleteApplicationException(exception);
                }
            }
        }
    }
}