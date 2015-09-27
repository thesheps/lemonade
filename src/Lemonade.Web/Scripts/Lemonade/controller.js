angular.module("lemonade")
    .controller("lemonadeController", ["$scope", "$http", function ($scope, $http) {
        $scope.loading = true;

        $http.get("/api/applications").then(function (res) {
            $scope.loading = false;
            $scope.applications = res.data;
            $.connection.lemonadeHub.client = new Lemonade($scope.applications, $scope.features);
            $.connection.hub.start();
        });

        $scope.addApplication = function(applicationName) {
            $.post("api/applications", { Name: applicationName });
        }

        $scope.deleteApplication = function (applicationId) {
            $.ajax({ url: 'api/applications?id=' + applicationId, type: 'DELETE' });
        }
    }]);