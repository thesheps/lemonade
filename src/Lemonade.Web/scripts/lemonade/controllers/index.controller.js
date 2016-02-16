angular.module("lemonade")
    .controller("indexController", ["$scope", "$http", "eventService", indexController]);

function indexController($scope, $http, eventService) {
    var hubProxy = $.connection.lemonadeHub;

    hubProxy.client.addApplication = function(application) {
        eventService.applicationAdded(application);
    }

    hubProxy.client.removeApplication = function(application) {
        eventService.applicationRemoved(application);
    }

    hubProxy.client.addFeature = function(feature) {
        eventService.featureAdded(feature);
    }

    hubProxy.client.removeFeature = function(feature) {
        eventService.featureRemoved(feature);
    }

    hubProxy.client.addFeatureOverride = function(featureOverride) {
        eventService.featureOverrideAdded(featureOverride);
    }

    hubProxy.client.removeFeatureOverride = function(featureOverride) {
        eventService.featureOverrideRemoved(featureOverride);
    }

    hubProxy.client.logError = function(error) {
        eventService.errorEncountered(error);
    }

    $.connection.hub.start();
}