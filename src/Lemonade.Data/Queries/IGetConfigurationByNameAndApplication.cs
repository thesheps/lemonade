using Lemonade.Data.Entities;

namespace Lemonade.Data.Queries
{
    public interface IGetConfigurationByNameAndApplication
    {
        Configuration Execute(string configurationName, string applicationName);
    }
}