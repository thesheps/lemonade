using System.Data.Common;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;

namespace Lemonade.Sql.Commands
{
    public class DeleteConfiguration : LemonadeConnection, IDeleteConfiguration
    {
        public DeleteConfiguration()
        {
        }

        public DeleteConfiguration(string connectionStringName) : base(connectionStringName)
        {
        }

        public void Execute(int configurationId)
        {
            using (var cnn = CreateConnection())
            {
                try
                {
                    cnn.Query("DELETE FROM Configuration WHERE ConfigurationId = @configurationId", new { configurationId });
                }
                catch (DbException exception)
                {
                    throw new DeleteConfigurationException(exception);
                }
            }
        }
    }
}