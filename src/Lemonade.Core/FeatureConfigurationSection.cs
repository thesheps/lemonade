using System.Configuration;

namespace Lemonade
{
    public class FeatureConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("FeatureResolver", IsRequired = true)]
        public string FeatureResolver => this["FeatureResolver"] as string;
    }
}