app.service("eventService", ["$rootScope", eventService]);

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

        applicationErrorEncountered: function (event) { subscribe("applicationErrorEncountered", event); },
        onApplicationErrorEncountered: function ($scope, callback) { handle($scope, "applicationErrorEncountered", callback); },

        featureAdded: function (event) { subscribe("featureAdded", event); },
        onFeatureAdded: function ($scope, callback) { handle($scope, "featureAdded", callback); },

        featureRemoved: function (event) { subscribe("featureRemoved", event); },
        onFeatureRemoved: function ($scope, callback) { handle($scope, "featureRemoved", callback); },

        featureErrorEncountered: function (event) { subscribe("featureErrorEncountered", event); },
        onFeatureErrorEncountered: function ($scope, callback) { handle($scope, "featureErrorEncountered", callback); },

        featureOverrideAdded: function (event) { subscribe("featureOverrideAdded", event); },
        onFeatureOverrideAdded: function ($scope, callback) { handle($scope, "featureOverrideAdded", callback); },

        featureOverrideRemoved: function (event) { subscribe("featureOverrideRemoved", event); },
        onFeatureOverrideRemoved: function ($scope, callback) { handle($scope, "featureOverrideRemoved", callback); },

        configurationAdded: function (event) { subscribe("configurationAdded", event); },
        onConfigurationAdded: function ($scope, callback) { handle($scope, "configurationAdded", callback); },

        configurationRemoved: function (event) { subscribe("configurationRemoved", event); },
        onConfigurationRemoved: function ($scope, callback) { handle($scope, "configurationRemoved", callback); },

        configurationErrorEncountered: function (event) { subscribe("configurationErrorEncountered", event); },
        onConfigurationErrorEncountered: function ($scope, callback) { handle($scope, "configurationErrorEncountered", callback); }
    }
}