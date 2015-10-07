using System;
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

        public static Feature Switches { get; } = new Feature();

        public bool this[Func<dynamic, dynamic> keyFunction] => this[keyFunction(_key)];

        public bool this[string key]
        {
            get
            {
                if (_featureResolver == null) _featureResolver = GetFeatureResolver();
                return _featureResolver.Resolve(key, ApplicationName);
            }
        }

        public bool Get<T>(Expression<Func<T, dynamic>> expression)
        {
            var uExpression = expression.Body as UnaryExpression;
            var mExpression = uExpression?.Operand as MemberExpression;

            return _featureResolver.Resolve(mExpression?.Member.Name, ApplicationName);
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

        private static IFeatureResolver GetFeatureResolver()
        {
            var featureConfiguration = FeatureConfigurationSection.Current;
            if (featureConfiguration == null)
                return new AppConfigFeatureResolver();

            var type = Type.GetType(featureConfiguration.FeatureResolver);
            if (type != null)
                return Activator.CreateInstance(type) as IFeatureResolver;

            return new AppConfigFeatureResolver();
        }

        private static string GetApplicationName()
        {
            return FeatureConfigurationSection.Current.ApplicationName ?? AppDomain.CurrentDomain.FriendlyName.Replace(".exe", string.Empty);
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
        private readonly DynamicKey _key = new DynamicKey();
        private static string _applicationName;
    }
}