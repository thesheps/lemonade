using System;

namespace Lemonade.Core.Services
{
    public interface ICacheProvider
    {
        T GetValue<T>(string key, Func<T> strategy);
    }
}