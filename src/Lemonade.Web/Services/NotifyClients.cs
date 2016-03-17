using Lemonade.Web.Core.Events;
using Lemonade.Web.Core.Services;
using Lemonade.Web.Infrastructure;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace Lemonade.Web.Services
{
    public class NotifyClients : INotifyClients
    {
        public NotifyClients(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void LogApplicationError(ApplicationErrorHasOccurred applicationErrorHasOccured)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.logApplicationError(applicationErrorHasOccured);
        }

        public void LogFeatureError(FeatureErrorHasOccurred featureErrorHasOccured)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.logFeatureError(featureErrorHasOccured);
        }

        public void LogFeatureOverrideError(FeatureOverrideErrorHasOccurred featureOverrideErrorHasOccured)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.logFeatureOverrideError(featureOverrideErrorHasOccured);
        }

        public void LogConfigurationError(ConfigurationErrorHasOccurred configurationErrorHasOccurred)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.logConfigurationError(configurationErrorHasOccurred);
        }

        public void LogResourceError(ResourceErrorHasOccurred resourceErrorHasOccurred)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.logResourceError(resourceErrorHasOccurred);
        }

        public void LogResourcesGenerated(ResourcesHaveBeenGenerated resourceErrorHasOccurred)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.logResourcesGenerated(resourceErrorHasOccurred);
        }

        public void AddApplication(ApplicationHasBeenCreated applicationHasBeenCreated)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.addApplication(applicationHasBeenCreated);
        }

        public void AddConfiguration(ConfigurationHasBeenCreated configurationHasBeenCreated)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.addConfiguration(configurationHasBeenCreated);
        }

        public void AddFeature(FeatureHasBeenCreated featureHasBeenCreated)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.addFeature(featureHasBeenCreated);
        }

        public void AddFeatureOverride(FeatureOverrideHasBeenCreated featureOverrideHasBeenCreated)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.addFeatureOverride(featureOverrideHasBeenCreated);
        }

        public void AddResource(ResourceHasBeenCreated resourceHasBeenCreated)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.addResource(resourceHasBeenCreated);
        }

        public void RemoveApplication(ApplicationHasBeenDeleted applicationHasBeenDeleted)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.removeApplication(applicationHasBeenDeleted);
        }

        public void RemoveConfiguration(ConfigurationHasBeenDeleted configurationHasBeenDeleted)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.removeConfiguration(configurationHasBeenDeleted);
        }

        public void RemoveFeature(FeatureHasBeenDeleted featureHasBeenDeleted)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.removeFeature(featureHasBeenDeleted);
        }

        public void RemoveFeatureOverride(FeatureOverrideHasBeenDeleted featureOverrideHasBeenDeleted)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.removeFeatureOverride(featureOverrideHasBeenDeleted);
        }

        public void RemoveResource(ResourceHasBeenDeleted resourceHasBeenDeleted)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.removeResource(resourceHasBeenDeleted);
        }

        public void UpdateApplication(ApplicationHasBeenUpdated applicationHasBeenUpdated)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.updateApplication(applicationHasBeenUpdated);
        }

        public void UpdateConfiguration(ConfigurationHasBeenUpdated configurationHasBeenUpdated)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.updateConfiguration(configurationHasBeenUpdated);
        }

        public void UpdateFeature(FeatureHasBeenUpdated featureHasBeenUpdated)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.updateFeature(featureHasBeenUpdated);
        }

        public void UpdateFeatureOverride(FeatureOverrideHasBeenUpdated featureOverrideHasBeenUpdated)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.updateFeatureOverride(featureOverrideHasBeenUpdated);
        }

        public void UpdateResource(ResourceHasBeenUpdated resourceHasBeenUpdated)
        {
            var hubContext = _connectionManager.GetHubContext<LemonadeHub>();
            hubContext.Clients.All.updateResource(resourceHasBeenUpdated);
        }

        private readonly IConnectionManager _connectionManager;
    }
}