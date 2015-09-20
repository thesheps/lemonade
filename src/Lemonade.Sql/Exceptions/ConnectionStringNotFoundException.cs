using System;

namespace Lemonade.Sql.Exceptions
{
    public class ConnectionStringNotFoundException : Exception
    {
        public ConnectionStringNotFoundException(string connectionString) 
            : base(string.Format(Errors.ConnectionStringNotFound, connectionString))
        {
        }
    }
}