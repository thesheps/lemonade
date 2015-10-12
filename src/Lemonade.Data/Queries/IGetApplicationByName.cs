using Lemonade.Data.Entities;

namespace Lemonade.Data.Queries
{
    public interface IGetApplicationByName
    {
        Application Execute(string applicationName);
    }
}