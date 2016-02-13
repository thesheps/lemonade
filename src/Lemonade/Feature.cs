using System;
using System.Dynamic;
using System.Linq.Expressions;

namespace Lemonade
{
    public class Feature
    {
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
            var key = "Feature" + Lemonade.ApplicationName + featureName;
            var value = Lemonade.CacheProvider
                .GetValue(key, () => Lemonade.FeatureResolver.Resolve(featureName, Lemonade.ApplicationName));

            return value;
        }

        private class DynamicKey : DynamicObject
        {
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                result = binder.Name;
                return true;
            }
        }

        private readonly DynamicKey _key = new DynamicKey();
    }
}