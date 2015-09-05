using System;

namespace Lemonade.Exceptions
{
    public class UnknownFeatureException : Exception
    {
        public UnknownFeatureException(string featureName) : base(string.Format(Errors.UnknownFeature, featureName))
        {
        }
    }
}