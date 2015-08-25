using System;
using System.Dynamic;
using Lemonade.Exceptions;
using Lemonade.Services;

namespace Lemonade
{
    public class Feature
    {
        public static Feature Switches { get; } = new Feature();

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

        public bool this[Func<dynamic, dynamic> keyFunction]
        {
            get
            {
                keyFunction(_key);
                return this[_key.Name];
            }
        }

        public void Execute(string key, Action action)
        {
            if (this[key]) action.Invoke();
        }

        public void Execute(Func<dynamic, dynamic> keyFunction, Action action)
        {
            if (this[keyFunction]) action.Invoke();
        }

        public static void SetResolver(IFeatureResolver featureResolver)
        {
            Switches._featureResolver = featureResolver;
        }

        private Feature()
        {
        }

        private class DynamicKey : DynamicObject
        {
            public string Name { get; private set; }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                Name = binder.Name;
                result = null;
                return true;
            }
        }

        private IFeatureResolver _featureResolver;
        private readonly DynamicKey _key = new DynamicKey();
    }
}