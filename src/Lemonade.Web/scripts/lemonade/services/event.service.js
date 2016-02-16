app.service("eventService", ["$rootScope", eventService]);
    
function eventService($rootScope) {
    var APPLICATION_ADDED = "applicationAdded";
    var applicationAdded = function (application) {
        $rootScope.$broadcast(APPLICATION_ADDED, {
            application: application
        });
    }

    var onApplicationAdded = function ($scope, handler) {
        $scope.$on(APPLICATION_ADDED, function (event, application) {
            handler(application);
        });
    }

    var APPLICATION_REMOVED = "applicationRemoved";
    var applicationRemoved = function (application) {
        $rootScope.$broadcast(APPLICATION_REMOVED, {
            application: application
        });
    }

    var onApplicationRemoved = function ($scope, handler) {
        $scope.$on(APPLICATION_REMOVED, function (event, application) {
            handler(application);
        });
    }

    var FEATURE_ADDED = "featureAdded";
    var featureAdded = function(feature) {
        $rootScope.$broadcast(FEATURE_ADDED, {
            feature: feature
        });
    }

    var onFeatureAdded = function ($scope, handler) {
        $scope.$on(FEATURE_ADDED, function (event, feature) {
            handler(feature);
        });
    }

    var FEATURE_REMOVED = "featureRemoved";
    var featureRemoved = function(feature) {
        $rootScope.$broadcast(FEATURE_REMOVED, {
            feature: feature
        });
    }

    var onFeatureRemoved = function ($scope, handler) {
        $scope.$on(FEATURE_REMOVED, function (event, feature) {
            handler(feature);
        });
    }

    var FEATURE_OVERRIDE_ADDED = "featureOverrideAdded";
    var featureOverrideAdded = function(featureOverride) {
        $rootScope.$broadcast(FEATURE_OVERRIDE_ADDED, {
            featureOverride: featureOverride
        });
    }

    var onFeatureOverrideAdded = function ($scope, handler) {
        $scope.$on(FEATURE_OVERRIDE_ADDED, function (event, featureOverride) {
            handler(featureOverride);
        });
    }

    var FEATURE_OVERRIDE_REMOVED = "featureOverrideRemoved";
    var featureOverrideRemoved = function(featureOverride) {
        $rootScope.$broadcast(FEATURE_OVERRIDE_REMOVED, {
            featureOverride: featureOverride
        });
    }

    var onFeatureOverrideRemoved = function ($scope, handler) {
        $scope.$on(FEATURE_OVERRIDE_REMOVED, function (event, featureOverride) {
            handler(featureOverride);
        });
    }

    var ERROR_ENCOUNTERED = "errorEncountered";
    var errorEncountered = function(message) {
        $rootScope.$broadcast(ERROR_ENCOUNTERED, {
            message: message
        });
    }

    var onErrorEncountered = function ($scope, handler) {
        $scope.$on(ERROR_ENCOUNTERED, function (event, message) {
            handler(message);
        });
    }

    return {
        applicationAdded: applicationAdded,
        onApplicationAdded: onApplicationAdded,
        applicationRemoved: applicationRemoved,
        onApplicationRemoved: onApplicationRemoved,
        featureAdded: featureAdded,
        onFeatureAdded: onFeatureAdded,
        featureRemoved: featureRemoved,
        onFeatureRemoved: onFeatureRemoved,
        featureOverrideAdded: featureOverrideAdded,
        onFeatureOverrideAdded: onFeatureOverrideAdded,
        featureOverrideRemoved: featureOverrideRemoved,
        onFeatureOverrideRemoved: onFeatureOverrideRemoved,
        errorEncountered: errorEncountered,
        onErrorEncountered: onErrorEncountered
    }
}