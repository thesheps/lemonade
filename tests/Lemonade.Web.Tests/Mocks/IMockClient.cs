namespace Lemonade.Web.Tests.Mocks
{
    public interface IMockClient
    {
        void addApplication(dynamic application);
        void addFeature(dynamic feature);
        void removeApplication(dynamic application);
        void removeFeature(dynamic feature);
    }
}