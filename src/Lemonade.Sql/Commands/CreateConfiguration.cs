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
                    cnn.Execute(@"INSERT INTO Configuration (Value, Name, ApplicationId)
                                  VALUES (@Value, @Name, @ApplicationId)", new
                    {
                        configuration.Value,
                        configuration.Name,
                        configuration.ApplicationId
                    });

                    configuration.ConfigurationId = cnn.Query<int>("SELECT CAST(@@IDENTITY AS INT)").First();
                }
                catch (DbException exception)
                {
                    throw new CreateConfigurationException(exception);
                }
            }
        }
    }
}