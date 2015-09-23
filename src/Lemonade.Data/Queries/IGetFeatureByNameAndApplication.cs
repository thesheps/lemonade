using Lemonade.Core.Entities;

namespace Lemonade.Core.Queries
{
    public interface IGetFeatureByNameAndApplication
    {
        Feature Execute(string featureName, string applicationName);
    }
}