using System;
using System.Collections.Generic;
using Lemonade.Core.Services;
using Polly;

namespace Lemonade.Services
{
    public class CacheProvider : ICacheProvider
    {
        public CacheProvider(double? cacheExpiration, int maximumRetries = 3)
        {
            _cacheExpiration = cacheExpiration;
            _maximumRetries = maximumRetries;
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

            var exception = Policy.Handle<Exception>()
                .Retry(_maximumRetries)
                .ExecuteAndCapture(() => result = strategy())
                .FinalException;

            if (exception != null) throw exception;

            return result;
        }

        private readonly Dictionary<string, Tuple<DateTime, object>> _values = new Dictionary<string, Tuple<DateTime, object>>();
        private readonly double? _cacheExpiration;
        private readonly int _maximumRetries;
    }
}