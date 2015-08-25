using System;

namespace Lemonade.Exceptions
{
    public class UnknownFeatureException : Exception
    {
        public UnknownFeatureException(string featureName) : base($"The specified feature {featureName} could not be identified.")
        {
        }
    }
}