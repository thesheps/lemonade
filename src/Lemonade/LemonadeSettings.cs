using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Lemonade
{
    public class LemonadeSettings
    {
        public static readonly LemonadeSettings Current = new LemonadeSettings();

        public string FeatureResolver => _configurationSection["FeatureResolver"];
        public string ConfigurationResolver => _configurationSection["ConfigurationResolver"];
        public string ResourceResolver => _configurationSection["ResourceResolver"];
        public string CacheProvider => _configurationSection["CacheProvider"];
        public string ApplicationName => _configurationSection["ApplicationName"];
        public string RetryPolicy => _configurationSection["RetryPolicy"];

        public double? CacheExpiration
        {
            get
            {
                double d;
                return double.TryParse(_configurationSection["CacheExpiration"], out d) ? d : 0;
            }
        }

        public int? MaximumAttempts
        {
            get
            {
                int i;
                return int.TryParse(_configurationSection["MaximumAttempts"], out i) ? i : 0;
            }
        }

        private LemonadeSettings()
        {
            var configuration = new ConfigurationBuilder().Add(new JsonConfigurationSource()).Build();
            _configurationSection = configuration.GetSection("AppSettings");
        }

        private readonly IConfigurationSection _configurationSection;
    }
}