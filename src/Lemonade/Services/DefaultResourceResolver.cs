using System.Globalization;
using System.Resources;
using System.Web.Compilation;
using Lemonade.Core.Services;

namespace Lemonade.Services
{
    public class DefaultResourceResolver : IResourceResolver
    {
        public IResourceProvider Create(ICacheProvider cacheProvider, string applicationName, string resourceSet)
        {
            return new DefaultResourceProvider(cacheProvider, applicationName, resourceSet);
        }

        private class DefaultResourceProvider : IResourceProvider
        {
            public IResourceReader ResourceReader { get; }

            public DefaultResourceProvider(ICacheProvider cacheProvider, string applicationName, string resourceSet)
            {
                _cacheProvider = cacheProvider;
                _applicationName = applicationName;
                _resourceSet = resourceSet;
            }

            public object GetObject(string resourceKey, CultureInfo culture)
            {
                var locale = (culture ?? CultureInfo.CurrentCulture).ThreeLetterWindowsLanguageName;
                var key = $"Resource{_applicationName}|{_resourceSet}|{resourceKey}|{locale}";

                return _cacheProvider.GetValue(key, () =>
                {
                    switch (locale)
                    {
                        case "ENG":
                            return "Hello World";
                        case "DEU":
                            return "Tag Weld";
                        case "FRA":
                            return "Bonjour le monde";
                        default:
                            return string.Empty;
                    }
                });
            }

            private readonly ICacheProvider _cacheProvider;
            private readonly string _applicationName;
            private readonly string _resourceSet;
        }
    }
}