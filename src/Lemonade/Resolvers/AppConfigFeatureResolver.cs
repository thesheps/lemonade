using System.Configuration;

namespace Lemonade.Resolvers
{
    public class AppConfigFeatureResolver : IFeatureResolver
    {
        public bool Resolve(string featureName)
        {
            bool enabled;
            return bool.TryParse(ConfigurationManager.AppSettings[featureName], out enabled) && enabled;
        }
    }
}