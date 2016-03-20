using System;

namespace Lemonade.Data.Exceptions
{
    public class CreateConfigurationException : Exception
    {
        public CreateConfigurationException(Exception innerException)
            : base(Errors.FailedToCreateConfiguration, innerException)
        {
        }
    }
}