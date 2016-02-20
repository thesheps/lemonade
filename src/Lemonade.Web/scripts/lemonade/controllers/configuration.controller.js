angular.module("lemonade")
    .controller("configurationController", ["$scope", "$http", "eventService", configurationController]);

function configurationController($scope, $http, eventService) {
    var configuration = new Configuration($scope);

    $http.get("/api/applications").then(function (res) {
        $scope.applications = res.data;
    });

    $scope.selectApplication = function (application) {
        $http.get("/api/configurations?applicationId=" + application.applicationId).then(function (res) {
            $scope.application = application;
            $scope.newConfiguration = { applicationId: application.applicationId }
            $scope.configurations = res.data;
        });
    }

    $scope.addConfiguration = function (configuration) {
        $http.post("/api/configurations", configuration);
    }

    $scope.updateConfiguration = function (configuration) {
        $http.put("/api/configurations", configuration);
    }

    $scope.deleteConfiguration = function (configurationId) {
        $.ajax({ url: "/api/configurations?id=" + configurationId, type: "DELETE" });
    }

    eventService.onConfigurationAdded($scope, configuration.addConfiguration);
    eventService.onConfigurationRemoved($scope, configuration.removeConfiguration);
    eventService.onConfigurationErrorEncountered($scope, configuration.logConfigurationError);
}