using System;

namespace Lemonade.Data.Exceptions
{
    public class DeleteFeatureException : Exception
    {
        public DeleteFeatureException(Exception innerException)
            : base(string.Format(Errors.FailedToDeleteFeature), innerException)
        {
        }
    }
}