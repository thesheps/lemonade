using System;

namespace Lemonade.Sql.Exceptions
{
    public class ConnectionStringNotFoundException : Exception
    {
        public ConnectionStringNotFoundException(string connectionStringName) : base(string.Format(Errors.ConnectionStringNotFound, connectionStringName))
        {
        }
    }
}