using System;

namespace Lemonade.Core.Exceptions
{
    public class DeleteFeatureException : Exception
    {
        public DeleteFeatureException(string featureName, Exception innerException)
            : base(string.Format(Errors.FailedToDeleteFeature, featureName, innerException.Message), innerException)
        {
        }
    }
}