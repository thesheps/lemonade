function eventService($rootScope) {
    var subscribe = function (name, event) {
        $rootScope.$broadcast(name, {
            event: event
        });
    }

    var handle = function ($scope, name, callback) {
        $scope.$on(name, function (event, message) {
            callback(message.event);
        });
    }
    
    return {
        applicationAdded: function (event) { subscribe("applicationAdded", event); },
        onApplicationAdded: function ($scope, callback) { handle($scope, "applicationAdded", callback); },

        applicationRemoved: function (event) { subscribe("applicationRemoved", event); },
        onApplicationRemoved: function ($scope, callback) { handle($scope, "applicationRemoved", callback); },

        applicationUpdated: function (event) { subscribe("applicationUpdated", event); },
        onApplicationUpdated: function ($scope, callback) { handle($scope, "applicationUpdated", callback); },

        applicationErrorEncountered: function (event) { subscribe("applicationErrorEncountered", event); },
        onApplicationErrorEncountered: function ($scope, callback) { handle($scope, "applicationErrorEncountered", callback); },

        featureAdded: function (event) { subscribe("featureAdded", event); },
        onFeatureAdded: function ($scope, callback) { handle($scope, "featureAdded", callback); },

        featureRemoved: function (event) { subscribe("featureRemoved", event); },
        onFeatureRemoved: function ($scope, callback) { handle($scope, "featureRemoved", callback); },

        featureUpdated: function (event) { subscribe("featureUpdated", event); },
        onFeatureUpdated: function ($scope, callback) { handle($scope, "featureUpdated", callback); },

        featureErrorEncountered: function (event) { subscribe("featureErrorEncountered", event); },
        onFeatureErrorEncountered: function ($scope, callback) { handle($scope, "featureErrorEncountered", callback); },

        featureOverrideAdded: function (event) { subscribe("featureOverrideAdded", event); },
        onFeatureOverrideAdded: function ($scope, callback) { handle($scope, "featureOverrideAdded", callback); },

        featureOverrideRemoved: function (event) { subscribe("featureOverrideRemoved", event); },
        onFeatureOverrideRemoved: function ($scope, callback) { handle($scope, "featureOverrideRemoved", callback); },

        featureOverrideUpdated: function (event) { subscribe("featureOverrideUpdated", event); },
        onFeatureOverrideUpdated: function ($scope, callback) { handle($scope, "featureOverrideUpdated", callback); },

        configurationAdded: function (event) { subscribe("configurationAdded", event); },
        onConfigurationAdded: function ($scope, callback) { handle($scope, "configurationAdded", callback); },

        configurationRemoved: function (event) { subscribe("configurationRemoved", event); },
        onConfigurationRemoved: function ($scope, callback) { handle($scope, "configurationRemoved", callback); },

        configurationUpdated: function (event) { subscribe("configurationUpdated", event); },
        onConfigurationUpdated: function ($scope, callback) { handle($scope, "configurationUpdated", callback); },

        configurationErrorEncountered: function (event) { subscribe("configurationErrorEncountered", event); },
        onConfigurationErrorEncountered: function ($scope, callback) { handle($scope, "configurationErrorEncountered", callback); },

        resourceAdded: function (event) { subscribe("resourceAdded", event); },
        onResourceAdded: function ($scope, callback) { handle($scope, "resourceAdded", callback); },

        resourceRemoved: function (event) { subscribe("resourceRemoved", event); },
        onResourceRemoved: function ($scope, callback) { handle($scope, "resourceRemoved", callback); },

        resourceUpdated: function (event) { subscribe("resourceUpdated", event); },
        onResourceUpdated: function ($scope, callback) { handle($scope, "resourceUpdated", callback); },

        resourceErrorEncountered: function (event) { subscribe("resourceErrorEncountered", event); },
        onResourceErrorEncountered: function ($scope, callback) { handle($scope, "resourceErrorEncountered", callback); }
    }
}