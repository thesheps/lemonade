using System;

namespace Lemonade.Data.Exceptions
{
    public class UpdateConfigurationException : Exception
    {
        public UpdateConfigurationException(Exception innerException)
            : base(string.Format(Errors.FailedToUpdateConfiguration), innerException)
        {
        }
    }
}