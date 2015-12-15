using System;
using System.Configuration;

namespace Lemonade
{
    public class LemonadeConfigurationSection : ConfigurationSection
    {
        public static readonly LemonadeConfigurationSection Current = (LemonadeConfigurationSection)ConfigurationManager.GetSection("Lemonade");

        [ConfigurationProperty("FeatureResolver", IsRequired = false)]
        public string FeatureResolver => this["FeatureResolver"] as string;

        [ConfigurationProperty("ConfigurationResolver", IsRequired = false)]
        public string ConfigurationResolver => this["ConfigurationResolver"] as string;

        [ConfigurationProperty("ApplicationName", IsRequired = false)]
        public string ApplicationName => this["ApplicationName"] as string ?? AppDomain.CurrentDomain.FriendlyName.Replace(".exe", string.Empty);

        [ConfigurationProperty("CacheExpiration", IsRequired = false)]
        public double? CacheExpiration => this["CacheExpiration"] as double?;
    }
}