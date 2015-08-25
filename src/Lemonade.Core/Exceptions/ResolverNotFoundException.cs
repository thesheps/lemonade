using System;

namespace Lemonade.Exceptions
{
    public class ResolverNotFoundException : Exception
    {
        public ResolverNotFoundException()
            : base("A feature resolver could not be found, please register one via the Resolver property.")
        {
        }
    }
}