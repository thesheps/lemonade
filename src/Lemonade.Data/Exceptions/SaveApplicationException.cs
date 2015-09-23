using System;

namespace Lemonade.Core.Exceptions
{
    public class SaveApplicationException : Exception
    {
        public SaveApplicationException(string applicationName, Exception innerException)
            : base(string.Format(Errors.FailedToSaveApplication, applicationName, innerException.Message), innerException)
        {
        }
    }
}