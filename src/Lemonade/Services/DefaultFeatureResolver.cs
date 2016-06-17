using Lemonade.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Lemonade.Services
{
    public class DefaultFeatureResolver : IFeatureResolver
    {
        public bool Resolve(string featureName, string applicationName)
        {
            bool enabled;

            var configuration = new ConfigurationBuilder().Add(new JsonConfigurationSource()).Build();
            var configurationSection = configuration.GetSection("AppSettings");

            return bool.TryParse(configurationSection[featureName], out enabled) && enabled;
        }
    }
}