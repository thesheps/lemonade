using System;
using System.Linq.Expressions;
using Lemonade.Core.Collections;
using Lemonade.Core.Services;

namespace Lemonade.Collections
{
    public class FeatureValueCollection : IFeatureValueCollection
    {
        public bool this[Func<object, dynamic> keyFunction] => _valueCollection[keyFunction];
        public bool this[string key] => _valueCollection[key];
        public bool Get<TExpression>(Expression<Func<TExpression, dynamic>> expression) { return _valueCollection.Get(expression); }

        public FeatureValueCollection(ICacheProvider cacheProvider, IFeatureResolver featureResolver, string applicationName)
        {
            _valueCollection = new ValueCollection<bool>((s) =>
            {
                var key = $"Feature{applicationName}{s}";
                return cacheProvider.GetValue(key, () => featureResolver.Resolve(s, applicationName));
            });
        }

        public void Execute(string key, Action action)
        {
            if (this[key]) action.Invoke();
        }

        public void Execute(Func<dynamic, dynamic> keyFunction, Action action)
        {
            if (this[keyFunction]) action.Invoke();
        }

        private readonly IValueCollection<bool> _valueCollection;
    }
}