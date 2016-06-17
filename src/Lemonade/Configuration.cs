using System;
using Lemonade.Core.Services;
using Lemonade.Services;

namespace Lemonade
{
    public class Configuration
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
            get { return _cacheProvider ?? (_cacheProvider = GetCacheProvider()); }
            set { _cacheProvider = value; }
        }

        public static IRetryPolicy RetryPolicy
        {
            get { return _retryPolicy ?? (_retryPolicy = GetRetryPolicy()); }
            set { _retryPolicy = value; }
        }

        public static string ApplicationName
        {
            get { return _applicationName ?? LemonadeSettings.Current.ApplicationName; }
            set { _applicationName = value; }
        }

        public static double? CacheExpiration
        {
            get { return _cacheExpiration ?? LemonadeSettings.Current.CacheExpiration; }
            set { _cacheExpiration = value; }
        }

        public static int? MaximumAttempts
        {
            get { return _maximumAttempts ?? LemonadeSettings.Current.MaximumAttempts; }
            set { _maximumAttempts = value; }
        }

        private static IFeatureResolver GetFeatureResolver()
        {
            var configuration = LemonadeSettings.Current;
            if (configuration == null) return new DefaultFeatureResolver();

            var type = Type.GetType(configuration.FeatureResolver);
            if (type != null) return Activator.CreateInstance(type) as IFeatureResolver;

            return new DefaultFeatureResolver();
        }

        private static IConfigurationResolver GetConfigurationResolver()
        {
            var configuration = LemonadeSettings.Current;
            if (configuration == null) return new DefaultConfigurationResolver();

            var type = Type.GetType(configuration.ConfigurationResolver);
            if (type != null) return Activator.CreateInstance(type) as IConfigurationResolver;

            return new DefaultConfigurationResolver();
        }

        private static ICacheProvider GetCacheProvider()
        {
            var configuration = LemonadeSettings.Current;
            if (configuration == null) return new DefaultCacheProvider(RetryPolicy, CacheExpiration);

            var type = Type.GetType(configuration.CacheProvider);
            if (type != null) return Activator.CreateInstance(type) as ICacheProvider;

            return new DefaultCacheProvider(RetryPolicy, CacheExpiration);
        }

        private static IRetryPolicy GetRetryPolicy()
        {
            var configuration = LemonadeSettings.Current;
            if (configuration == null) return new DefaultRetryPolicy(MaximumAttempts.GetValueOrDefault());

            var type = Type.GetType(configuration.CacheProvider);
            if (type != null) return Activator.CreateInstance(type) as IRetryPolicy;

            return new DefaultRetryPolicy(MaximumAttempts.GetValueOrDefault());
        }

        private static int? _maximumAttempts;
        private static double? _cacheExpiration;
        private static string _applicationName;
        private static IFeatureResolver _featureResolver;
        private static IConfigurationResolver _configurationResolver;
        private static ICacheProvider _cacheProvider;
        private static IRetryPolicy _retryPolicy;
    }
}