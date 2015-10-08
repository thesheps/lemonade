using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using Lemonade.Resolvers;

namespace Lemonade
{
    public class Feature
    {
        public static IFeatureResolver Resolver
        {
            get { return _featureResolver ?? (_featureResolver = GetFeatureResolver()); }
            set { _featureResolver = value; }
        }

        public static string ApplicationName
        {
            get { return _applicationName ?? (_applicationName = GetApplicationName()); }
            set { _applicationName = value; }
        }

        public static double? CacheExpiration
        {
            get { return _cacheExpiration ?? GetCacheExpiration(); }
            set { _cacheExpiration = value; }
        }

        public static Feature Switches { get; } = new Feature();

        public bool this[Func<dynamic, dynamic> keyFunction] => GetFeatureSwitch(keyFunction(_key));

        public bool this[string key] => GetFeatureSwitch(key);

        public bool Get<T>(Expression<Func<T, dynamic>> expression)
        {
            var uExpression = expression.Body as UnaryExpression;
            var mExpression = uExpression?.Operand as MemberExpression;
            return GetFeatureSwitch(mExpression?.Member.Name);
        }

        public void Execute(string key, Action action)
        {
            if (this[key]) action.Invoke();
        }

        public void Execute(Func<dynamic, dynamic> keyFunction, Action action)
        {
            if (this[keyFunction]) action.Invoke();
        }

        private Feature()
        {
        }

        private static bool GetFeatureSwitch(string featureName)
        {
            var key = ApplicationName + featureName;

            Tuple<DateTime, bool> featureTuple;
            if (!_features.TryGetValue(key, out featureTuple))
                featureTuple = new Tuple<DateTime, bool>(DateTime.Now, Resolver.Resolve(featureName, ApplicationName));
            else if (featureTuple.Item1.AddMinutes(GetCacheExpiration().GetValueOrDefault()) > DateTime.Now)
                featureTuple = new Tuple<DateTime, bool>(DateTime.Now, Resolver.Resolve(featureName, ApplicationName));

            _features[key] = featureTuple;

            return featureTuple.Item2;
        }

        private static IFeatureResolver GetFeatureResolver()
        {
            var featureConfiguration = FeatureConfigurationSection.Current;
            if (featureConfiguration == null) return new AppConfigFeatureResolver();

            var type = Type.GetType(featureConfiguration.FeatureResolver);
            if (type != null) return Activator.CreateInstance(type) as IFeatureResolver;

            return new AppConfigFeatureResolver();
        }

        private static string GetApplicationName()
        {
            return FeatureConfigurationSection.Current.ApplicationName ?? AppDomain.CurrentDomain.FriendlyName.Replace(".exe", string.Empty);
        }

        private static double? GetCacheExpiration()
        {
            return FeatureConfigurationSection.Current.CacheExpiration;
        }

        private class DynamicKey : DynamicObject
        {
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                result = binder.Name;
                return true;
            }
        }

        private static IFeatureResolver _featureResolver;
        private static double? _cacheExpiration;
        private static string _applicationName;
        private readonly DynamicKey _key = new DynamicKey();
        private static readonly Dictionary<string, Tuple<DateTime, bool>> _features = new Dictionary<string, Tuple<DateTime, bool>>();
    }
}