using System;
using System.Dynamic;
using System.Reflection;
using Lemonade.Exceptions;
using Lemonade.Services;

namespace Lemonade
{
    public class Feature
    {
        public bool this[Func<dynamic, dynamic> keyFunction] => this[keyFunction(_key)];

        public bool this[string key]
        {
            get
            {
                if (_featureResolver == null) throw new ResolverNotFoundException();
                var isEnabled = _featureResolver.Get(key);
                
                if (!isEnabled.HasValue) throw new UnknownFeatureException(key);
                return isEnabled.Value;
            }
        }

        public static Feature Switches { get; } = new Feature();

        public static void SetResolver(IFeatureResolver featureResolver)
        {
            Switches._featureResolver = featureResolver;
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