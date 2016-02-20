using System;

namespace Lemonade.Data.Exceptions
{
    public class CreateConfigurationException : Exception
    {
        public CreateConfigurationException(Exception innerException)
            : base(string.Format(Errors.FailedToCreateConfiguration), innerException)
        {
        }
    }
}