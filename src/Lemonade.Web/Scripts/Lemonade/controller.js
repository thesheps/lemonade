angular.module("lemonade")
    .controller("lemonadeController", ["$scope", "$http", function ($scope, $http) {
        $http.get("/api/applications").then(function (res) {
            $scope.applications = res.data;
            $.connection.lemonadeHub.client = new Lemonade($scope);
            $.connection.hub.start();
        });

        $scope.selectApplication = function (applicationId) {
            $http.get("/api/features?applicationId=" + applicationId).then(function (res) {
                $scope.newFeature = { ApplicationId: applicationId }
                $scope.features = res.data;
            });
        }

        $scope.addApplication = function (applicationName) {
            $.post("api/applications", { Name: applicationName });
        }

        $scope.deleteApplication = function (applicationId) {
            $.ajax({ url: 'api/applications?id=' + applicationId, type: 'DELETE' });
        }

        $scope.addFeature = function() {
            $http.post("/api/features", $scope.newFeature);
        }

        $scope.deleteFeature = function (featureId) {
            $.ajax({ url: 'api/features?id=' + featureId, type: 'DELETE' });
        }
    }]);