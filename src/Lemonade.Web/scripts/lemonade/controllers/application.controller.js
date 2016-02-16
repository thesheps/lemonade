angular.module("lemonade")
    .controller("applicationController", ["$scope", "$http", "eventService", applicationController]);

function applicationController($scope, $http, eventService) {
    var application = new Application($scope);

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

    eventService.onApplicationAdded($scope, application.addApplication);
    eventService.onApplicationRemoved($scope, application.removeApplication);
    eventService.onErrorEncountered($scope, application.logError);
}