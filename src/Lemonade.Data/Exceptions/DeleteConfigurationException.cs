using System;

namespace Lemonade.Data.Exceptions
{
    public class DeleteConfigurationException : Exception
    {
        public DeleteConfigurationException(Exception innerException)
            : base(string.Format(Errors.FailedToDeleteConfiguration), innerException)
        {
        }
    }
}