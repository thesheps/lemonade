using System;
using System.Globalization;
using System.Resources;
using System.Web.Compilation;
using Lemonade.Core.Services;

namespace Lemonade.Services
{
    public class HttpResourceResolver : IResourceResolver
    {
        public IResourceProvider Create(string resourceSet)
        {
            return new HttpResourceProvider(resourceSet);
        }

        private class HttpResourceProvider : IResourceProvider
        {
            public IResourceReader ResourceReader { get; }

            public HttpResourceProvider(string resourceSet)
            {
                _resourceSet = resourceSet;
            }

            public object GetObject(string resourceKey, CultureInfo culture)
            {
                throw new NotImplementedException();
            }

            private readonly string _resourceSet;
        }
    }
}