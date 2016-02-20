using Lemonade.Collections;
using Lemonade.Core.Collections;

namespace Lemonade
{
    public class Config
    {
        public static T Settings<T>(string key) => Settings<T>()[key];
        public static IConfigurationValueCollection<T> Settings<T>() => new ConfigurationValueCollection<T>(Configuration.CacheProvider, Configuration.ConfigurationResolver, Configuration.ApplicationName);
    }
}