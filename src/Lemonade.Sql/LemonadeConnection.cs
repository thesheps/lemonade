using System.Configuration;
using System.Data.Common;
using Lemonade.Sql.Exceptions;

namespace Lemonade.Sql
{
    public abstract class LemonadeConnection
    {
        protected LemonadeConnection() : this("Lemonade")
        {
        }

        protected LemonadeConnection(string connectionStringName)
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (connectionStringSettings == null) throw new ConnectionStringNotFoundException(connectionStringName);

            DbProviderFactory = DbProviderFactories.GetFactory(connectionStringSettings.ProviderName);
            ConnectionString = connectionStringSettings.ConnectionString;
        }

        protected DbConnection CreateConnection()
        {
            var cnn = DbProviderFactory.CreateConnection();
            if (cnn == null) return null;

            cnn.ConnectionString = ConnectionString;
            cnn.Open();

            return cnn;
        }

        protected DbProviderFactory DbProviderFactory { get; }
        protected string ConnectionString { get; }
    }
}