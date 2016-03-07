using System.Data.Common;
using System.Linq;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;

namespace Lemonade.Sql.Commands
{
    public class CreateConfiguration : LemonadeConnection, ICreateConfiguration
    {
        public CreateConfiguration()
        {
        }

        public CreateConfiguration(string connectionStringName) : base(connectionStringName)
        {
        }

        public void Execute(Data.Entities.Configuration configuration)
        {
            using (var cnn = CreateConnection())
            {
                try
                {
                    configuration.ConfigurationId =
                        cnn.Query<int>(@"INSERT INTO Configuration (Value, Name, ApplicationId)
                                         VALUES (@Value, @Name, @ApplicationId);
                                         SELECT SCOPE_IDENTITY();", new
                        {
                            configuration.Value,
                            configuration.Name,
                            configuration.ApplicationId
                        }).First();
                }
                catch (DbException exception)
                {
                    throw new CreateConfigurationException(exception);
                }
            }
        }
    }
}