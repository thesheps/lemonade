function configurationController($scope, $http, eventService, toastService) {
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

    $scope.onConfigurationAdded = function (configuration) {
        $scope.$apply(function () {
            $scope.newConfiguration = { applicationId: $scope.application.applicationId }
            $scope.configurations.push(configuration);
        });
    }

    $scope.onConfigurationRemoved = function (configuration) {
        $scope.$apply(function () {
            for (var i = 0; i < $scope.configurations.length; i++) {
                if ($scope.configurations[i].configurationId === configuration.configurationId) {
                    $scope.configurations.splice(i, 1);
                }
            }

            $scope.configuration = null;
        });
    }

    $scope.onConfigurationUpdated = function () {
        toastService.toast("Successfully updated!", "OK", "bottom right");
    }

    $scope.onConfigurationErrorEncountered = function (error) {
        toastService.toast(error.message, "OK", "bottom right");
    }

    eventService.onConfigurationAdded($scope, $scope.onConfigurationAdded);
    eventService.onConfigurationRemoved($scope, $scope.onConfigurationRemoved);
    eventService.onConfigurationUpdated($scope, $scope.onConfigurationUpdated);
    eventService.onConfigurationErrorEncountered($scope, $scope.onConfigurationErrorEncountered);
}