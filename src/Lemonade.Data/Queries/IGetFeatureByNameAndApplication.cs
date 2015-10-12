using Lemonade.Data.Entities;

namespace Lemonade.Data.Queries
{
    public interface IGetFeatureByNameAndApplication
    {
        Feature Execute(string featureName, string applicationName);
    }
}