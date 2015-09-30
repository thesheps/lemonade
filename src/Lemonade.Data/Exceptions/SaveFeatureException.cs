using System;

namespace Lemonade.Core.Exceptions
{
    public class SaveFeatureException : Exception
    {
        public SaveFeatureException(Exception innerException)
            : base(string.Format(Errors.FailedToSaveFeature), innerException)
        {
        }
    }
}