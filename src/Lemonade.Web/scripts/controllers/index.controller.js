function indexController($scope, $http, eventService) {
    var hubProxy = $.connection.lemonadeHub;

    hubProxy.client.addApplication = function(application) {
        eventService.applicationAdded(application);
    }

    hubProxy.client.removeApplication = function(application) {
        eventService.applicationRemoved(application);
    }

    hubProxy.client.updateApplication = function (application) {
        eventService.applicationUpdated(application);
    }

    hubProxy.client.logApplicationError = function (error) {
        eventService.applicationErrorEncountered(error);
    }

    hubProxy.client.logResourcesGenerated = function(resources) {
        eventService.resourcesGenerated(resources);
    }

    hubProxy.client.addFeature = function(feature) {
        eventService.featureAdded(feature);
    }

    hubProxy.client.removeFeature = function(feature) {
        eventService.featureRemoved(feature);
    }

    hubProxy.client.updateFeature = function (feature) {
        eventService.featureUpdated(feature);
    }

    hubProxy.client.logFeatureError = function (error) {
        eventService.featureErrorEncountered(error);
    }

    hubProxy.client.addFeatureOverride = function(featureOverride) {
        eventService.featureOverrideAdded(featureOverride);
    }

    hubProxy.client.removeFeatureOverride = function(featureOverride) {
        eventService.featureOverrideRemoved(featureOverride);
    }

    hubProxy.client.updateFeatureOverride = function (featureOverride) {
        eventService.featureOverrideUpdated(featureOverride);
    }

    hubProxy.client.addConfiguration= function (configuration) {
        eventService.configurationAdded(configuration);
    }

    hubProxy.client.removeConfiguration = function (configuration) {
        eventService.configurationRemoved(configuration);
    }

    hubProxy.client.updateConfiguration = function (configuration) {
        eventService.configurationUpdated(configuration);
    }

    hubProxy.client.logConfigurationError = function (error) {
        eventService.configurationErrorEncountered(error);
    }

    hubProxy.client.addResource = function (resource) {
        eventService.resourceAdded(resource);
    }

    hubProxy.client.removeResource = function (resource) {
        eventService.resourceRemoved(resource);
    }

    hubProxy.client.updateResource = function (resource) {
        eventService.resourceUpdated(resource);
    }

    hubProxy.client.logResourceError = function (error) {
        eventService.resourceErrorEncountered(error);
    }

    $.connection.hub.start();
}