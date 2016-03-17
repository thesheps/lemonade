using Lemonade.Web.Core.Events;

namespace Lemonade.Web.Core.Services
{
    public interface INotifyClients
    {
        void LogApplicationError(ApplicationErrorHasOccurred applicationErrorHasOccured);
        void LogFeatureError(FeatureErrorHasOccurred featureErrorHasOccured);
        void LogFeatureOverrideError(FeatureOverrideErrorHasOccurred featureOverrideErrorHasOccured);
        void LogConfigurationError(ConfigurationErrorHasOccurred configurationErrorHasOccurred);
        void LogResourceError(ResourceErrorHasOccurred resourceErrorHasOccurred);
        void LogResourcesGenerated(ResourcesHaveBeenGenerated @event);
        void AddApplication(ApplicationHasBeenCreated applicationHasBeenCreated);
        void AddConfiguration(ConfigurationHasBeenCreated configurationHasBeenCreated);
        void AddFeature(FeatureHasBeenCreated featureHasBeenCreated);
        void AddFeatureOverride(FeatureOverrideHasBeenCreated featureOverrideHasBeenCreated);
        void AddResource(ResourceHasBeenCreated resourceHasBeenCreated);
        void RemoveApplication(ApplicationHasBeenDeleted applicationHasBeenDeleted);
        void RemoveConfiguration(ConfigurationHasBeenDeleted configurationHasBeenDeleted);
        void RemoveFeature(FeatureHasBeenDeleted featureHasBeenDeleted);
        void RemoveFeatureOverride(FeatureOverrideHasBeenDeleted featureOverrideHasBeenDeleted);
        void RemoveResource(ResourceHasBeenDeleted resourceHasBeenDeleted);
        void UpdateApplication(ApplicationHasBeenUpdated applicationHasBeenUpdated);
        void UpdateConfiguration(ConfigurationHasBeenUpdated configurationHasBeenUpdated);
        void UpdateFeature(FeatureHasBeenUpdated featureHasBeenUpdated);
        void UpdateFeatureOverride(FeatureOverrideHasBeenUpdated featureOverrideHasBeenUpdated);
        void UpdateResource(ResourceHasBeenUpdated resourceHasBeenUpdated);
    }
}