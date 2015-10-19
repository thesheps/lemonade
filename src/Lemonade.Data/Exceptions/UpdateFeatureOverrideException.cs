using System;

namespace Lemonade.Data.Exceptions
{
    public class UpdateFeatureOverrideException : Exception
    {
        public UpdateFeatureOverrideException(Exception innerException)
            : base(string.Format(Errors.FailedToCreateFeatureOverride), innerException)
        {
        }
    }
}