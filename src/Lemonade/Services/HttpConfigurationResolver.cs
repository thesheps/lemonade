using System;
using Lemonade.Core.Services;

namespace Lemonade.Services
{
    public class HttpConfigurationResolver : IConfigurationResolver
    {
        public HttpConfigurationResolver(string lemonadeServiceUrl)
        {
            _lemonadeServiceUrl = lemonadeServiceUrl;
        }

        public T Resolve<T>(string configurationName, string applicationName)
        {
            throw new NotImplementedException();
        }

        private readonly string _lemonadeServiceUrl;
    }
}