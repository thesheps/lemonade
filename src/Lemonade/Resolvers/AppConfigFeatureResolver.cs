using System.Configuration;
using Lemonade.Core;

namespace Lemonade.Resolvers
{
    public class AppConfigFeatureResolver : IFeatureResolver
    {
        public bool Get(string featureName)
        {
            bool enabled;
            return bool.TryParse(ConfigurationManager.AppSettings[featureName], out enabled) && enabled;
        }
    }
}