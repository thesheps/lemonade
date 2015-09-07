using System;
using System.Configuration;
using System.Dynamic;
using Lemonade.Exceptions;

namespace Lemonade
{
    public class Feature
    {
        public bool this[Func<dynamic, dynamic> keyFunction] => this[keyFunction(_key)];

        public bool this[string key]
        {
            get
            {
                if (_featureResolver == null)
                    _featureResolver = GetFeatureResolver();

                var isEnabled = _featureResolver.Get(key);

                if (!isEnabled.HasValue)
                    throw new UnknownFeatureException(key);

                return isEnabled.Value;
            }
        }

        public static Feature Switches { get; } = new Feature();

        public static IFeatureResolver Resolver
        {
            get { return Switches._featureResolver; }
            set { Switches._featureResolver = value; }
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
            var featureConfiguration = ConfigurationManager.GetSection("FeatureConfiguration") as FeatureConfigurationSection;

            if (featureConfiguration == null) return new AppConfigFeatureResolver();
            var type = Type.GetType(featureConfiguration.FeatureResolver);
            if (type != null) return Activator.CreateInstance(type) as IFeatureResolver;

            return new AppConfigFeatureResolver();
        }

        private class DynamicKey : DynamicObject
        {
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                result = binder.Name;
                return true;
            }
        }

        private IFeatureResolver _featureResolver;
        private readonly DynamicKey _key = new DynamicKey();
    }
}