using System.Data.Common;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;

namespace Lemonade.Sql.Commands
{
    public class DeleteResource : LemonadeConnection, IDeleteResource
    {
        public DeleteResource()
        {
        }

        public DeleteResource(string connectionStringName) : base(connectionStringName)
        {
        }

        public void Execute(int resourceId)
        {
            using (var cnn = CreateConnection())
            {
                try
                {
                    cnn.Query("DELETE FROM Resource WHERE ResourceId = @resourceId", new { resourceId });
                }
                catch (DbException exception)
                {
                    throw new DeleteResourceException(exception);
                }
            }
        }
    }
}