angular.module("lemonade")
    .controller("featureController", ["$scope", "$http", "eventService", featureController]);

function featureController($scope, $http, eventService) {
    $http.get("/api/applications").then(function (res) {
        $scope.applications = res.data;
    });

    $scope.selectApplication = function (application) {
        $http.get("/api/features?applicationId=" + application.applicationId).then(function (res) {
            $scope.application = application;
            $scope.newFeature = { applicationId: application.applicationId }
            $scope.features = res.data;
        });
    }

    $scope.addFeature = function (feature) {
        $http.post("/api/features", feature);
    }

    $scope.updateFeature = function (feature) {
        $http.put("/api/features", feature);
    }

    $scope.deleteFeature = function (featureId) {
        $.ajax({ url: "/api/features?id=" + featureId, type: "DELETE" });
    }

    $scope.addFeatureOverride = function (featureOverride) {
        $http.post("/api/featureoverrides", featureOverride);
    }

    $scope.updateFeatureOverride = function (featureOverride) {
        $http.put("/api/featureoverrides", featureOverride);
    }

    $scope.deleteFeatureOverride = function (featureOverrideId) {
        $.ajax({ url: "/api/featureoverrides?id=" + featureOverrideId, type: "DELETE" });
    }

    var handleAddFeature = function (message) {
        $scope.$apply(function () {
            $scope.features.push(message.feature);
        });
    }
    
    var handleRemoveFeature = function (message) {
        $scope.$apply(function () {
            for (var i = 0; i < $scope.features.length; i++) {
                if ($scope.features[i].featureId === message.feature.featureId) {
                    $scope.features.splice(i, 1);
                    return;
                }
            }
        });
    }

    var handleAddFeatureOverride = function (message) {
        $scope.$apply(function () {
            for (var i = 0; i < $scope.features.length; i++) {
                if ($scope.features[i].featureId === message.featureOverride.featureId) {
                    $scope.features[i].featureOverrides.push(message.featureOverride);
                    return;
                }
            }
        });
    }

    var handleRemoveFeatureOverride = function (message) {
        $scope.$apply(function () {
            for (var x = 0; x < $scope.features.length; x++) {
                var feature = $scope.features[x];

                for (var y = 0; y < feature.featureOverrides.length; y++) {
                    if (feature.featureOverrides[y].featureOverrideId === message.featureOverride.featureOverrideId) {
                        feature.featureOverrides.splice(y, 1);
                        return;
                    }
                }
            }
        });
    }

    var handleErrorEncountered = function (message) {
        $.bootstrapGrowl(message.error.errorMessage, { type: "danger" });
    }

    eventService.onFeatureAdded($scope, handleAddFeature);
    eventService.onFeatureRemoved($scope, handleRemoveFeature);
    eventService.onFeatureOverrideAdded($scope, handleAddFeatureOverride);
    eventService.onFeatureOverrideRemoved($scope, handleRemoveFeatureOverride);
    eventService.onErrorEncountered($scope, handleErrorEncountered);
}