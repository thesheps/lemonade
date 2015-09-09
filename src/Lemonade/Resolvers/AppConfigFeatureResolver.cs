using System.Configuration;

namespace Lemonade.Resolvers
{
    public class AppConfigFeatureResolver : IFeatureResolver
    {
        public bool Get(string value)
        {
            bool enabled;
            return bool.TryParse(ConfigurationManager.AppSettings[value], out enabled) && enabled;
        }
    }
}