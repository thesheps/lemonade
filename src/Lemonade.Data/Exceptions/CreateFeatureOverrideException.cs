using System;

namespace Lemonade.Data.Exceptions
{
    public class CreateFeatureOverrideException : Exception
    {
        public CreateFeatureOverrideException(Exception innerException)
            : base(Errors.FailedToCreateFeatureOverride, innerException)
        {
        }
    }
}