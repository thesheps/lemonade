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

    var CONFIGURATION_REMOVED = "configurationRemoved";
    var configurationRemoved = function (configuration) {
        $rootScope.$broadcast(CONFIGURATION_REMOVED, {
            configuration: configuration
        });
    }

    var onConfigurationRemoved = function ($scope, handler) {
        $scope.$on(CONFIGURATION_REMOVED, function (event, configuration) {
            handler(configuration);
        });
    }

    var CONFIGURATION_ADDED = "configurationAdded";
    var configurationAdded = function (configuration) {
        $rootScope.$broadcast(CONFIGURATION_ADDED, {
            configuration: configuration
        });
    }

    var onConfigurationAdded = function ($scope, handler) {
        $scope.$on(CONFIGURATION_ADDED, function (event, configuration) {
            handler(configuration);
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

    var APPLICATION_ERROR_ENCOUNTERED = "applicationErrorEncountered";
    var applicationErrorEncountered = function (message) {
        $rootScope.$broadcast(APPLICATION_ERROR_ENCOUNTERED, {
            message: message
        });
    }

    var onApplicationErrorEncountered = function ($scope, handler) {
        $scope.$on(APPLICATION_ERROR_ENCOUNTERED, function (event, message) {
            handler(message);
        });
    }

    var FEATURE_ERROR_ENCOUNTERED = "featureErrorEncountered";
    var featureErrorEncountered = function (message) {
        $rootScope.$broadcast(FEATURE_ERROR_ENCOUNTERED, {
            message: message
        });
    }

    var onFeatureErrorEncountered = function ($scope, handler) {
        $scope.$on(FEATURE_ERROR_ENCOUNTERED, function (event, message) {
            handler(message);
        });
    }

    var CONFIGURATION_ERROR_ENCOUNTERED = "configurationErrorEncountered";
    var configurationErrorEncountered = function (message) {
        $rootScope.$broadcast(CONFIGURATION_ERROR_ENCOUNTERED, {
            message: message
        });
    }

    var onConfigurationErrorEncountered = function ($scope, handler) {
        $scope.$on(CONFIGURATION_ERROR_ENCOUNTERED, function (event, message) {
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
        configurationAdded: configurationAdded,
        onConfigurationAdded: onConfigurationAdded,
        configurationRemoved: configurationRemoved,
        onConfigurationRemoved: onConfigurationRemoved,
        featureOverrideAdded: featureOverrideAdded,
        onFeatureOverrideAdded: onFeatureOverrideAdded,
        featureOverrideRemoved: featureOverrideRemoved,
        onFeatureOverrideRemoved: onFeatureOverrideRemoved,
        configurationErrorEncountered: configurationErrorEncountered,
        onConfigurationErrorEncountered: onConfigurationErrorEncountered,
        featureErrorEncountered: featureErrorEncountered,
        onFeatureErrorEncountered: onFeatureErrorEncountered,
        applicationErrorEncountered: applicationErrorEncountered,
        onApplicationErrorEncountered: onApplicationErrorEncountered
    }
}