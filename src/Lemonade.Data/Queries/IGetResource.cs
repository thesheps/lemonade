using Lemonade.Data.Entities;

namespace Lemonade.Data.Queries
{
    public interface IGetResource
    {
        Resource Execute(string application, string resourceSet, string resourceKey, string locale);
    }
}