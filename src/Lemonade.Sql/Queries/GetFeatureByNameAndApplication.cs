using System.Data.Common;
using System.Linq;
using Dapper;
using Lemonade.Data.Queries;

namespace Lemonade.Sql.Queries
{
    public class GetFeatureByNameAndApplication : LemonadeConnection, IGetFeatureByNameAndApplication
    {
        public GetFeatureByNameAndApplication()
        {
        }

        public GetFeatureByNameAndApplication(string connectionStringName) : base(connectionStringName)
        {
        }

        public GetFeatureByNameAndApplication(DbProviderFactory dbProviderFactory, string connectionString) : base(dbProviderFactory, connectionString)
        {
        }

        public Data.Entities.Feature Execute(string featureName, string applicationName)
        {
            using (var cnn = DbProviderFactory.CreateConnection())
            {
                if (cnn == null) return null;

                cnn.ConnectionString = ConnectionString;

                return cnn.Query<Data.Entities.Feature>("SELECT * FROM Feature WHERE FeatureName = @featureName " +
                                                        "AND ApplicationName = @applicationName",
                    new
                    {
                        featureName,
                        applicationName
                    }).SingleOrDefault();
            }
        }
    }
}