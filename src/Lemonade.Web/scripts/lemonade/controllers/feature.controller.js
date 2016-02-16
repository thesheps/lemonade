angular.module("lemonade")
    .controller("featureController", ["$scope", "$http", "eventService", featureController]);

function featureController($scope, $http, eventService) {
    var feature = new Feature($scope);

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

    eventService.onFeatureAdded($scope, feature.addFeature);
    eventService.onFeatureRemoved($scope, feature.removeFeature);
    eventService.onFeatureOverrideAdded($scope, feature.addFeatureOverride);
    eventService.onFeatureOverrideRemoved($scope, feature.removeFeatureOverride);
    eventService.onErrorEncountered($scope, feature.logError);
}