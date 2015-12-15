using System;
using System.Collections.Generic;
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
            var key = Lemonade.ApplicationName + featureName;

            Tuple<DateTime, bool> feature;
            if (!Features.TryGetValue(key, out feature))
                feature = new Tuple<DateTime, bool>(DateTime.Now, Lemonade.FeatureResolver.Resolve(featureName, Lemonade.ApplicationName));
            else if (feature.Item1.AddMinutes(Lemonade.CacheExpiration.GetValueOrDefault()) <= DateTime.Now)
                feature = new Tuple<DateTime, bool>(DateTime.Now, Lemonade.FeatureResolver.Resolve(featureName, Lemonade.ApplicationName));

            Features[key] = feature;

            return feature.Item2;
        }

        private class DynamicKey : DynamicObject
        {
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                result = binder.Name;
                return true;
            }
        }

        private static readonly Dictionary<string, Tuple<DateTime, bool>> Features = new Dictionary<string, Tuple<DateTime, bool>>();
        private readonly DynamicKey _key = new Feature.DynamicKey();
    }
}