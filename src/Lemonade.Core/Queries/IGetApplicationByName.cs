using Lemonade.Core.Domain;

namespace Lemonade.Core.Queries
{
    public interface IGetApplicationByName
    {
        Application Execute(string applicationName);
    }
}