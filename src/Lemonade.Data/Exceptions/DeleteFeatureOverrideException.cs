using System;

namespace Lemonade.Data.Exceptions
{
    public class DeleteFeatureOverrideException : Exception
    {
        public DeleteFeatureOverrideException(Exception innerException)
            : base(string.Format(Errors.FailedToDeleteFeatureOverride), innerException)
        {
        }
    }
}