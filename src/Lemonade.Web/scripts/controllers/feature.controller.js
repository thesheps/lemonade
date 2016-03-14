function featureController($scope, $http, eventService, toastService) {
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
        $http.post("/api/features", feature)
            .then(function() {
                $scope.newFeature = { applicationId: $scope.application.applicationId }
            });
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

    $scope.onFeatureAdded = function (feature) {
        $scope.$apply(function () {
            $scope.features.push(feature);
        });
    }

    $scope.onFeatureRemoved = function (feature) {
        $scope.$apply(function () {
            for (var i = 0; i < $scope.features.length; i++) {
                if ($scope.features[i].featureId === feature.featureId) {
                    $scope.features.splice(i, 1);
                    return;
                }
            }
        });
    }

    $scope.onFeatureUpdated = function () {
        toastService.toast("Successfully updated!", "OK", "bottom right");
    }

    $scope.onFeatureOverrideAdded = function (featureOverride) {
        $scope.$apply(function () {
            for (var i = 0; i < $scope.features.length; i++) {
                if ($scope.features[i].featureId === featureOverride.featureId) {
                    $scope.features[i].featureOverrides.push(featureOverride);
                    return;
                }
            }
        });
    }

    $scope.onFeatureOverrideRemoved = function (featureOverride) {
        $scope.$apply(function () {
            for (var x = 0; x < $scope.features.length; x++) {
                var feature = $scope.features[x];

                for (var y = 0; y < feature.featureOverrides.length; y++) {
                    if (feature.featureOverrides[y].featureOverrideId === featureOverride.featureOverrideId) {
                        feature.featureOverrides.splice(y, 1);
                        return;
                    }
                }
            }
        });
    }

    $scope.onFeatureOverrideUpdated = function () {
        toastService.toast("Successfully updated!", "OK", "bottom right");
    }

    $scope.onFeatureErrorEncountered = function (error) {
        toastService.toast(error.message, "OK", "bottom right");
    }

    eventService.onFeatureAdded($scope, $scope.onFeatureAdded);
    eventService.onFeatureRemoved($scope, $scope.onFeatureRemoved);
    eventService.onFeatureUpdated($scope, $scope.onFeatureUpdated);
    eventService.onFeatureOverrideAdded($scope, $scope.onFeatureOverrideAdded);
    eventService.onFeatureOverrideRemoved($scope, $scope.onFeatureOverrideRemoved);
    eventService.onFeatureErrorEncountered($scope, $scope.onFeatureErrorEncountered);
    eventService.onFeatureOverrideUpdated($scope, $scope.onFeatureOverrideUpdated);
}