using Lemonade.Core.Entities;

namespace Lemonade.Core.Queries
{
    public interface IGetApplicationByName
    {
        Application Execute(string applicationName);
    }
}