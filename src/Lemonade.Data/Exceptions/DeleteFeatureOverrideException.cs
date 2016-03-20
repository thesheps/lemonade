using System;

namespace Lemonade.Data.Exceptions
{
    public class DeleteFeatureOverrideException : Exception
    {
        public DeleteFeatureOverrideException(Exception innerException)
            : base(Errors.FailedToDeleteFeatureOverride, innerException)
        {
        }
    }
}