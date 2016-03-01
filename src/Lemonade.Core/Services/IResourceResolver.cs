using System.Web.Compilation;

namespace Lemonade.Core.Services
{
    public interface IResourceResolver
    {
        IResourceProvider Create(ICacheProvider cacheProvider, string applicationName, string resourceSet);
    }
}