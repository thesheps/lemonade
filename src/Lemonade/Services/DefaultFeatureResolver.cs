using System.Configuration;
using Lemonade.Core.Services;

namespace Lemonade.Services
{
    public class DefaultFeatureResolver : IFeatureResolver
    {
        public bool Resolve(string featureName, string applicationName)
        {
            bool enabled;
            return bool.TryParse(ConfigurationManager.AppSettings[featureName], out enabled) && enabled;
        }
    }
}