angular.module("lemonade")
    .controller("lemonadeController", ["$scope", "$http", function ($scope, $http) {
        $http.get("/api/applications").then(function (res) {
            $scope.applications = res.data;
            $.connection.lemonadeHub.client = new Lemonade($scope);
            $.connection.hub.start();
        });

        $scope.selectApplication = function (application) {
            $http.get("/api/features?applicationId=" + application.applicationId).then(function (res) {
                $scope.application = application;
                $scope.newFeature = { applicationId: application.applicationId }
                $scope.features = res.data;
            });
        }

        $scope.addApplication = function (applicationName) {
            $.post("/api/applications", { name: applicationName });
        }

        $scope.updateApplication = function (application) {
            $http.put("/api/applications", application);
        }

        $scope.deleteApplication = function (applicationId) {
            $.ajax({ url: "/api/applications?id=" + applicationId, type: "DELETE" });
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

        $scope.updateFeature = function (featureOverride) {
            $http.put("/api/featureoverrides", featureOverride);
        }

        $scope.deleteFeatureOverride = function (featureOverrideId) {
            $.ajax({ url: "/api/featureoverrides?id=" + featureOverrideId, type: "DELETE" });
        }
    }]);