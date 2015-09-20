using System;

namespace Lemonade.Sql.Exceptions
{
    public class SaveFeatureException : Exception
    {
        public SaveFeatureException(string featureName, Exception innerException)
            : base(string.Format(Errors.FailedToSaveFeature, featureName), innerException)
        {
        }
    }
}