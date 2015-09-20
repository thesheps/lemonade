using System;
using System.Data.Common;

namespace Lemonade.Sql.Exceptions
{
    public class SaveApplicationException : Exception
    {
        public SaveApplicationException(string applicationName, Exception innerException)
            : base(string.Format(Errors.FailedToSaveApplication, applicationName), innerException)
        {
        }
    }
}