function indexController($scope, $http, eventService) {
    var hubProxy = $.connection.lemonadeHub;

    hubProxy.client.addApplication = function(application) {
        eventService.applicationAdded(application);
    }

    hubProxy.client.removeApplication = function(application) {
        eventService.applicationRemoved(application);
    }

    hubProxy.client.logApplicationError = function (error) {
        eventService.applicationErrorEncountered(error);
    }

    hubProxy.client.addFeature = function(feature) {
        eventService.featureAdded(feature);
    }

    hubProxy.client.removeFeature = function(feature) {
        eventService.featureRemoved(feature);
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

    hubProxy.client.addConfiguration= function (configuration) {
        eventService.configurationAdded(configuration);
    }

    hubProxy.client.removeConfiguration = function (configuration) {
        eventService.configurationRemoved(configuration);
    }

    hubProxy.client.logConfigurationError = function (error) {
        eventService.configurationErrorEncountered(error);
    }

    $.connection.hub.start();
}