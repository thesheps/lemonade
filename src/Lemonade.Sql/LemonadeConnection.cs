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

        protected LemonadeConnection(DbProviderFactory dbProviderFactory, string connectionString)
        {
            DbProviderFactory = dbProviderFactory;
            ConnectionString = connectionString;
        }

        protected DbProviderFactory DbProviderFactory { get; private set; }
        protected string ConnectionString { get; private set; }
    }
}