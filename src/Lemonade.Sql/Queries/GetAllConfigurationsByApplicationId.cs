using System.Collections.Generic;
using System.Linq;
using Dapper;
using Lemonade.Data.Entities;
using Lemonade.Data.Queries;

namespace Lemonade.Sql.Queries
{
    public class GetAllConfigurationsByApplicationId : LemonadeConnection, IGetAllConfigurationsByApplicationId
    {
        public GetAllConfigurationsByApplicationId()
        {
        }

        public GetAllConfigurationsByApplicationId(string connectionStringName) : base(connectionStringName)
        {
        }

        public IList<Data.Entities.Configuration> Execute(int applicationId)
        {
            using (var cnn = CreateConnection())
            {
                var results = cnn.Query<Data.Entities.Configuration, Application, Data.Entities.Configuration>(
                    @"SELECT * FROM Configuration c INNER JOIN Application a ON c.ApplicationId = a.ApplicationId
                      WHERE a.ApplicationId = @applicationId",
                    (c, a) =>
                    {
                        c.Application = a;
                        return c;
                    },
                    new { applicationId },
                    splitOn: "ApplicationId");

                return results.ToList();
            }
        }
    }
}