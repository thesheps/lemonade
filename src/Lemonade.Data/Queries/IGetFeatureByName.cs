using Lemonade.Data.Entities;

namespace Lemonade.Data.Queries
{
    public interface IGetFeatureByName
    {
        Feature Execute(string featureName);
    }
}