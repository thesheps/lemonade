using System;

namespace Lemonade.Exceptions
{
    public class ResolverNotFoundException : Exception
    {
        public ResolverNotFoundException() : base(Errors.ResolverNotFound)
        {
        }
    }
}