using System;
using System.Collections.Generic;
using Lemonade.Core.Services;

namespace Lemonade.Services
{
    public class DefaultCacheProvider : ICacheProvider
    {
        public DefaultCacheProvider(IRetryPolicy retryPolicy, double? cacheExpiration)
        {
            _retryPolicy = retryPolicy;
            _cacheExpiration = cacheExpiration;
        }

        public T GetValue<T>(string key, Func<T> strategy)
        {
            Tuple<DateTime, object> value;
            if (!_values.TryGetValue(key, out value))
                value = new Tuple<DateTime, object>(DateTime.Now, GetValue(strategy));
            else if (value.Item1.AddMinutes(_cacheExpiration.GetValueOrDefault()) <= DateTime.Now)
                value = new Tuple<DateTime, object>(DateTime.Now, GetValue(strategy));

            _values[key] = value;

            return (T)value.Item2;
        }

        private T GetValue<T>(Func<T> strategy)
        {
            var result = default(T);

            _retryPolicy.Execute(() => result = strategy());

            return result;
        }

        private readonly Dictionary<string, Tuple<DateTime, object>> _values = new Dictionary<string, Tuple<DateTime, object>>();
        private readonly double? _cacheExpiration;
        private readonly IRetryPolicy _retryPolicy;
    }
}