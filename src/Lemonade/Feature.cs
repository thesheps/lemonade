using Lemonade.Collections;
using Lemonade.Core.Collections;

namespace Lemonade
{
    public class Feature
    {
        public static IFeatureValueCollection Switches => new FeatureValueCollection(Configuration.CacheProvider, Configuration.FeatureResolver, Configuration.ApplicationName);
    }
}
