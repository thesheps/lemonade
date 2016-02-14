using System;
using System.Configuration;

namespace Lemonade.Resolvers
{
    public class AppConfigConfigurationResolver : IConfigurationResolver
    {
        public T Resolve<T>(string configurationName, string applicationName)
        {
            var value = ConfigurationManager.AppSettings[configurationName];
            if (typeof(T) == typeof(Uri))
                return ((T)(object)new Uri(value));

            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}