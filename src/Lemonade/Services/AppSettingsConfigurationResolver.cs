using System;
using System.Configuration;
using Lemonade.Core.Services;

namespace Lemonade.Services
{
    public class AppSettingsConfigurationResolver : IConfigurationResolver
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