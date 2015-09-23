using System;

namespace Lemonade.Core.Exceptions
{
    public class SaveFeatureException : Exception
    {
        public SaveFeatureException(string featureName, Exception innerException)
            : base(string.Format(Errors.FailedToSaveFeature, featureName, innerException.Message), innerException)
        {
        }
    }
}