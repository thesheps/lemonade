using System;

namespace Lemonade
{
    public class Feature : Value<bool>
    {
        public static Feature Switches { get; } = new Feature();

        public void Execute(string key, Action action)
        {
            if (this[key]) action.Invoke();
        }

        public void Execute(Func<dynamic, dynamic> keyFunction, Action action)
        {
            if (this[keyFunction]) action.Invoke();
        }

        protected override bool GetValue(string key, string applicationName)
        {
            return Lemonade.FeatureResolver.Resolve(key, applicationName);
        }

        protected override string ValueType => "Feature";
    }
}