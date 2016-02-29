using System.Web.Compilation;

namespace Lemonade.Core.Services
{
    public interface IResourceResolver
    {
        IResourceProvider Create(string resourceSet);
    }
}