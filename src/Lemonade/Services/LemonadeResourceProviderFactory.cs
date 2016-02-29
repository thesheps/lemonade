using System.Globalization;
using System.Resources;
using System.Threading;
using System.Web.Compilation;

namespace Lemonade.Services
{
    public class LemonadeResourceProviderFactory : ResourceProviderFactory
    {
        public override IResourceProvider CreateGlobalResourceProvider(string classKey)
        {
            return new HttpResourceProvider(classKey);
        }

        public override IResourceProvider CreateLocalResourceProvider(string virtualPath)
        {
            return new HttpResourceProvider(virtualPath);
        }

        private class HttpResourceProvider : IResourceProvider
        {
            public IResourceReader ResourceReader { get; }

            public HttpResourceProvider(string classKey)
            {
                _classKey = classKey;
            }

            public object GetObject(string resourceKey, CultureInfo culture)
            {
                var cultureName = (culture ?? Thread.CurrentThread.CurrentCulture).ThreeLetterWindowsLanguageName;

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

            private readonly string _classKey;
        }
    }
}