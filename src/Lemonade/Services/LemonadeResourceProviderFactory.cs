using System.Web.Compilation;

namespace Lemonade.Services
{
    public class LemonadeResourceProviderFactory : ResourceProviderFactory
    {
        public override IResourceProvider CreateGlobalResourceProvider(string classKey)
        {
            return CreateResourceProvider(classKey);
        }

        public override IResourceProvider CreateLocalResourceProvider(string virtualPath)
        {
            return CreateResourceProvider(virtualPath);
        }

        private static IResourceProvider CreateResourceProvider(string resourceSet)
        {
            return Configuration.ResourceResolver.Create(resourceSet);
        }
    }
}