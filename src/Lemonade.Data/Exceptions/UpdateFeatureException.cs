using System;

namespace Lemonade.Core.Exceptions
{
    public class UpdateFeatureException : Exception
    {
        public UpdateFeatureException(Exception innerException)
            : base(string.Format(Errors.FailedToCreateFeature), innerException)
        {
        }
    }
}