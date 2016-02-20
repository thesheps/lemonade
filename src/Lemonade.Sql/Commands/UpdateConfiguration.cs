using System.Data.Common;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;

namespace Lemonade.Sql.Commands
{
    public class UpdateConfiguration : LemonadeConnection, IUpdateConfiguration
    {
        public UpdateConfiguration()
        {
        }

        public UpdateConfiguration(string connectionStringName) : base(connectionStringName)
        {
        }

        public void Execute(Data.Entities.Configuration configuration)
        {
            using (var cnn = CreateConnection())
            {
                try
                {
                    cnn.Execute(@"UPDATE Configuration
                                  SET Value = @Value, Name = @Name
                                  WHERE ConfigurationId = @ConfigurationId", new
                    {
                        configuration.Value,
                        configuration.Name,
                        configuration.ConfigurationId
                    });
                }
                catch (DbException exception)
                {
                    throw new UpdateConfigurationException(exception);
                }
            }
        }
    }
}