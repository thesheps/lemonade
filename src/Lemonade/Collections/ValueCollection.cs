using System;
using System.Dynamic;
using System.Linq.Expressions;
using Lemonade.Core.Collections;

namespace Lemonade.Collections
{
    sealed internal class ValueCollection<T> : IValueCollection<T>
    {
        public T this[Func<dynamic, dynamic> keyFunction] => _getValue(keyFunction(_key));
        public T this[string key] => _getValue(key);

        public ValueCollection(Func<string, T> getValue)
        {
            _getValue = getValue;
        }

        public T Get<TExpression>(Expression<Func<TExpression, dynamic>> expression)
        {
            var uExpression = expression.Body as UnaryExpression;
            var mExpression = uExpression?.Operand as MemberExpression;
            return _getValue(mExpression?.Member.Name);
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
        private readonly Func<string, T> _getValue;
    }
}