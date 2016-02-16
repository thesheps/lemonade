angular.module("lemonade")
    .controller("applicationController", ["$scope", "$http", "eventService", applicationController]);

function applicationController($scope, $http, eventService) {
    $http.get("/api/applications").then(function(res) {
        $scope.applications = res.data;
    });

    $scope.addApplication = function (applicationName) {
        $.post("/api/applications", { name: applicationName });
    }

    $scope.updateApplication = function (application) {
        $http.put("/api/applications", application);
    }

    $scope.deleteApplication = function (applicationId) {
        $.ajax({ url: "/api/applications?id=" + applicationId, type: "DELETE" });
    }

    var handleAddApplication = function (message) {
        $scope.$apply(function () {
            $scope.applications.push(message.application);
        });
    }

    var handleRemoveApplication = function (message) {
        $scope.$apply(function () {
            for (var i = 0; i < $scope.applications.length; i++) {
                if ($scope.applications[i].applicationId === message.application.applicationId) {
                    $scope.applications.splice(i, 1);
                }
            }

            $scope.application = null;
            $scope.features = [];
        });
    }

    var handleErrorEncountered = function (message) {
        $.bootstrapGrowl(message.error.errorMessage, { type: "danger" });
    }

    eventService.onApplicationAdded($scope, handleAddApplication);
    eventService.onApplicationRemoved($scope, handleRemoveApplication);
    eventService.onErrorEncountered($scope, handleErrorEncountered);
}