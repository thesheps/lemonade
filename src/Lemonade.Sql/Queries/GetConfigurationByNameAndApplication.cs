using System.Linq;
using Dapper;
using Lemonade.Data.Entities;
using Lemonade.Data.Queries;

namespace Lemonade.Sql.Queries
{
    public class GetConfigurationByNameAndApplication : LemonadeConnection, IGetConfigurationByNameAndApplication
    {
        public GetConfigurationByNameAndApplication()
        {
        }

        public GetConfigurationByNameAndApplication(string connectionStringName) : base(connectionStringName)
        {
        }

        public Data.Entities.Configuration Execute(string configurationName, string applicationName)
        {
            using (var cnn = CreateConnection())
            {
                var configuration = cnn.Query<Data.Entities.Configuration, Application, Data.Entities.Configuration>(
                    @"SELECT * FROM Configuration c INNER JOIN Application a ON c.ApplicationId = a.ApplicationId
                      WHERE c.Name = @configurationName AND a.Name = @applicationName",
                    (c, a) => { c.Application = a; return c; },
                    new {configurationName, applicationName },
                    splitOn: "ApplicationId").FirstOrDefault();

                return configuration;
            }
        }
    }
}