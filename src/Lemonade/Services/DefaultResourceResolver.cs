using System.Globalization;
using System.Resources;
using System.Web.Compilation;
using Lemonade.Core.Services;

namespace Lemonade.Services
{
    public class DefaultResourceResolver : IResourceResolver
    {
        public IResourceProvider Create(string resourceSet)
        {
            return new DefaultResourceProvider(resourceSet);
        }

        private class DefaultResourceProvider : IResourceProvider
        {
            public IResourceReader ResourceReader { get; }

            public DefaultResourceProvider(string resourceSet)
            {
                _resourceSet = resourceSet;
            }

            public object GetObject(string resourceKey, CultureInfo culture)
            {
                var cultureName = (culture ?? CultureInfo.CurrentCulture).ThreeLetterWindowsLanguageName;

                switch (cultureName)
                {
                    case "ENG":
                        return "Hello World";
                    case "DEU":
                        return "Tag Weld";
                    case "FRA":
                        return "Bonjour le monde";
                }

                return string.Empty;
            }

            private readonly string _resourceSet;
        }
    }
}