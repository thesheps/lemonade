using System;
using Lemonade.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Lemonade.Services
{
    public class DefaultConfigurationResolver : IConfigurationResolver
    {
        public T Resolve<T>(string configurationName, string applicationName)
        {
            var configuration = new ConfigurationBuilder().Add(new JsonConfigurationSource()).Build();
            var configurationSection = configuration.GetSection("AppSettings");

            var value = configurationSection[configurationName];
            if (typeof(T) == typeof(Uri))
                return ((T)(object)new Uri(value));

            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}