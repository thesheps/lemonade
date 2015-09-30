using System;

namespace Lemonade.Core.Exceptions
{
    public class SaveApplicationException : Exception
    {
        public SaveApplicationException(Exception innerException)
            : base(string.Format(Errors.FailedToSaveApplication), innerException)
        {
        }
    }
}