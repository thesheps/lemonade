using Lemonade.Exceptions;
using Lemonade.Services;

namespace Lemonade
{
    public class Feature
    {
        public static Feature Switches { get; } = new Feature();

        public bool this[string value]
        {
            get
            {
                if (_featureResolver == null) throw new ResolverNotFoundException();
                var isEnabled = _featureResolver.Get(value);

                if (!isEnabled.HasValue) throw new UnknownFeatureException(value);
                return isEnabled.Value;
            }
        }

        public static void SetResolver(IFeatureResolver featureResolver)
        {
            Switches._featureResolver = featureResolver;
        }

        private Feature()
        {
        }

        private IFeatureResolver _featureResolver;
    }
}