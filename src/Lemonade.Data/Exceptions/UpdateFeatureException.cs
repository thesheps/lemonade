using System;

namespace Lemonade.Data.Exceptions
{
    public class UpdateFeatureException : Exception
    {
        public UpdateFeatureException(Exception innerException)
            : base(string.Format(Errors.FailedToCreateFeature), innerException)
        {
        }
    }
}