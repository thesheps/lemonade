using System;

namespace Lemonade.Data.Exceptions
{
    public class DeleteConfigurationException : Exception
    {
        public DeleteConfigurationException(Exception innerException)
            : base(Errors.FailedToDeleteConfiguration, innerException)
        {
        }
    }
}