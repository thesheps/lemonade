using System.Configuration;

namespace Lemonade
{
    public class FeatureConfigurationSection : ConfigurationSection
    {
        public static readonly FeatureConfigurationSection Current = (FeatureConfigurationSection)ConfigurationManager.GetSection("FeatureConfiguration");

        [ConfigurationProperty("FeatureResolver", IsRequired = true)]
        public string FeatureResolver => this["FeatureResolver"] as string;

        [ConfigurationProperty("ApplicationName", IsRequired = false)]
        public string ApplicationName => this["ApplicationName"] as string;
    }
}