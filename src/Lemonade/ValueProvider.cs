using System;
using System.Dynamic;
using System.Linq.Expressions;

namespace Lemonade
{
    public abstract class ValueProvider<T>
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

        protected ValueProvider()
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

        protected abstract T GetValue(string key, string applicationName);
        protected abstract string ValueType { get; }
        private readonly DynamicKey _key = new DynamicKey();
    }
}