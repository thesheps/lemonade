function EventService() {
    return {
        onApplicationAdded: function ($scope, callback) { },
        onApplicationRemoved: function ($scope, callback) { },
        onApplicationErrorEncountered: function ($scope, callback) { },
        onFeatureAdded: function ($scope, callback) { },
        onFeatureRemoved: function ($scope, callback) { },
        onFeatureErrorEncountered: function ($scope, callback) { },
        onFeatureOverrideAdded: function ($scope, callback) { },
        onFeatureOverrideRemoved: function ($scope, callback) { },
        onConfigurationAdded: function ($scope, callback) { },
        onConfigurationRemoved: function ($scope, callback) { },
        onConfigurationErrorEncountered: function ($scope, callback) { },
    }
}