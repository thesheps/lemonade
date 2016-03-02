using System;

namespace Lemonade.Data.Exceptions
{
    public class UpdateResourceException : Exception
    {
        public UpdateResourceException(Exception innerException)
            : base(string.Format(Errors.FailedToCreateFeature), innerException)
        {
        }
    }
}