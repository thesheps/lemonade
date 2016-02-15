angular.module("lemonade")
    .controller("applicationController", ["$scope", "$http", applicationController]);

function applicationController($scope, $http) {
    $http.get("/api/applications").then(function (res) {
        $scope.applications = res.data;
        $.connection.applicationHub.client = new Application($scope);
        $.connection.hub.start();
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
}