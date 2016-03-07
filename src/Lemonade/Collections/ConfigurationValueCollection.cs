using System;
using System.Linq.Expressions;
using Lemonade.Core.Collections;
using Lemonade.Core.Services;

namespace Lemonade.Collections
{
    public class ConfigurationValueCollection<T> : IConfigurationValueCollection<T>
    {
        public T this[Func<object, object> keyFunction] => _valueCollection[keyFunction];
        public T this[string key] => _valueCollection[key];
        public T Get<TExpression>(Expression<Func<TExpression, object>> expression) { return _valueCollection.Get(expression); }

        public ConfigurationValueCollection(ICacheProvider cacheProvider, IConfigurationResolver configurationResolver, string applicationName)
        {
            _valueCollection = new ValueCollection<T>((s) =>
            {
                var key = $"Config{applicationName}{s}";
                return cacheProvider.GetValue(key, () => configurationResolver.Resolve<T>(s, applicationName));
            });
        }

        private readonly ValueCollection<T> _valueCollection;
    }
}