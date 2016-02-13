using System;
using Lemonade.Resolvers;

namespace Lemonade
{
    public class Lemonade
    {
        public static IFeatureResolver FeatureResolver
        {
            get { return _featureResolver ?? (_featureResolver = GetFeatureResolver()); }
            set { _featureResolver = value; }
        }

        public static IConfigurationResolver ConfigurationResolver
        {
            get { return _configurationResolver ?? (_configurationResolver = GetConfigurationResolver()); }
            set { _configurationResolver = value; }
        }

        public static ICacheProvider CacheProvider
        {
            get { return _cacheProvider ?? (_cacheProvider = new CacheProvider(_cacheExpiration)); }
            set { _cacheProvider = value; }
        }

        public static string ApplicationName
        {
            get { return _applicationName ?? LemonadeConfigurationSection.Current.ApplicationName; }
            set { _applicationName = value; }
        }

        public static double? CacheExpiration
        {
            get { return _cacheExpiration ?? LemonadeConfigurationSection.Current.CacheExpiration; }
            set { _cacheExpiration = value; }
        }

        private static IFeatureResolver GetFeatureResolver()
        {
            var featureConfiguration = LemonadeConfigurationSection.Current;
            if (featureConfiguration == null) return new AppConfigFeatureResolver();

            var type = Type.GetType(featureConfiguration.FeatureResolver);
            if (type != null) return Activator.CreateInstance(type) as IFeatureResolver;

            return new AppConfigFeatureResolver();
        }

        private static IConfigurationResolver GetConfigurationResolver()
        {
            var featureConfiguration = LemonadeConfigurationSection.Current;
            if (featureConfiguration == null) return new AppConfigConfigurationResolver();

            var type = Type.GetType(featureConfiguration.ConfigurationResolver);
            if (type != null) return Activator.CreateInstance(type) as IConfigurationResolver;

            return new AppConfigConfigurationResolver();
        }

        private static ICacheProvider _cacheProvider;
        private static IConfigurationResolver _configurationResolver;
        private static IFeatureResolver _featureResolver;
        private static string _applicationName;
        private static double? _cacheExpiration;
    }
}