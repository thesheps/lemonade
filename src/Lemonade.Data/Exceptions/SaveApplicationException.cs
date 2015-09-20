using System;

namespace Lemonade.Data.Exceptions
{
    public class SaveApplicationException : Exception
    {
        public SaveApplicationException(string applicationName, Exception innerException)
            : base(string.Format(Errors.FailedToSaveApplication, applicationName), innerException)
        {
        }
    }
}