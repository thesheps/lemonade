using System;

namespace Lemonade.Web.Core.Exceptions
{
    public class UnsupportedTranslationException : Exception
    {
        public UnsupportedTranslationException(string type) : base(type)
        {
        }
    }
}