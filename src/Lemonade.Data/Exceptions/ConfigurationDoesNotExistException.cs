using System;

namespace Lemonade.Data.Exceptions
{
    public class ConfigurationDoesNotExistException : Exception
    {
        public ConfigurationDoesNotExistException()
            : base(Errors.ConfigurationDoesNotExist)
        {
        }
    }
}