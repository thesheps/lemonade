using System;

namespace Lemonade.Data.Exceptions
{
    public class SaveFeatureException : Exception
    {
        public SaveFeatureException(string featureName, Exception innerException)
            : base(string.Format(Errors.FailedToSaveFeature, featureName), innerException)
        {
        }
    }
}