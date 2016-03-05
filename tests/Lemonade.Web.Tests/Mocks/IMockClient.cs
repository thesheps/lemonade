namespace Lemonade.Web.Tests.Mocks
{
    public interface IMockClient
    {
        void addApplication(dynamic application);
        void removeApplication(dynamic application);
        void updateApplication(dynamic application);
        void addFeature(dynamic feature);
        void removeFeature(dynamic feature);
        void updateFeature(object any);
        void addFeatureOverride(dynamic featureOverride);
        void removeFeatureOverride(dynamic featureOverride);
        void updateFeatureOverride(dynamic featureOverride);
        void addConfiguration(dynamic configuration);
        void removeConfiguration(dynamic configuration);
        void updateConfiguration(dynamic configuration);
        void addResource(dynamic resource);
        void removeResource(dynamic resource);
        void updateResource(dynamic resource);
        void logFeatureError(dynamic feature);
    }
}