using System;
using System.Linq.Expressions;

namespace Lemonade
{
    public abstract class Value<T>
    {
        public T this[Func<dynamic, dynamic> keyFunction] => GetValue(keyFunction(_key));

        public T this[string key] => GetValue(key);

        public T Get<TExpression>(Expression<Func<TExpression, dynamic>> expression)
        {
            var uExpression = expression.Body as UnaryExpression;
            var mExpression = uExpression?.Operand as MemberExpression;
            return GetValue(mExpression?.Member.Name);
        }

        protected T GetValue(string keyName)
        {
            var key = ValueType + Lemonade.ApplicationName + keyName;
            var value = Lemonade.CacheProvider
                .GetValue(key, () => GetValue(keyName, Lemonade.ApplicationName));

            return value;
        }

        protected Value()
        {
        }

        protected abstract T GetValue(string key, string applicationName);
        protected abstract string ValueType { get; }
        private readonly DynamicKey _key = new DynamicKey();
    }
}