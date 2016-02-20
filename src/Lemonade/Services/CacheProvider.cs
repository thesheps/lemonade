using System;
using System.Collections.Generic;
using Lemonade.Core.Services;

namespace Lemonade.Services
{
    public class CacheProvider : ICacheProvider
    {
        public CacheProvider(double? cacheExpiration)
        {
            _cacheExpiration = cacheExpiration;
        }

        public T GetValue<T>(string key, Func<T> strategy)
        {
            Tuple<DateTime, object> value;
            if (!_values.TryGetValue(key, out value))
                value = new Tuple<DateTime, object>(DateTime.Now, strategy());
            else if (value.Item1.AddMinutes(_cacheExpiration.GetValueOrDefault()) <= DateTime.Now)
                value = new Tuple<DateTime, object>(DateTime.Now, strategy());

            _values[key] = value;

            return (T)value.Item2;
        }

        private readonly Dictionary<string, Tuple<DateTime, object>> _values = new Dictionary<string, Tuple<DateTime, object>>();
        private readonly double? _cacheExpiration;
    }
}