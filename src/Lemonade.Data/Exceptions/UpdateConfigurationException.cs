using System;

namespace Lemonade.Data.Exceptions
{
    public class UpdateConfigurationException : Exception
    {
        public UpdateConfigurationException(Exception innerException)
            : base(Errors.FailedToUpdateConfiguration, innerException)
        {
        }
    }
}