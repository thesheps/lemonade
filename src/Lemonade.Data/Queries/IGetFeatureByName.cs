namespace Lemonade.Data.Queries
{
    public interface IGetFeatureByName
    {
        Entities.Feature Execute(string name);
    }
}