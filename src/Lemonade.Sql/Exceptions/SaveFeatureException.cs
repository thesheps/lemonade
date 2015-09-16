using System;

namespace Lemonade.Sql.Exceptions
{
    public class SaveFeatureException : Exception
    {
        public SaveFeatureException(string applicationName, string featureName, Exception innerException)
            : base(string.Format(Errors.FailedToSaveFeature, featureName, applicationName), innerException)
        {
        }
    }
}