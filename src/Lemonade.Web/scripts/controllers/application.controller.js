function applicationController($scope, $http, eventService, toastService) {
    $http.get("/api/applications").then(function (res) {
        $scope.applications = res.data;
    });

    $scope.addApplication = function (applicationName) {
        $.post("/api/applications", { name: applicationName })
            .then(function() {
                $scope.applicationName = "";
            });
    }

    $scope.updateApplication = function (application) {
        $http.put("/api/applications", application);
    }

    $scope.deleteApplication = function (applicationId) {
        $.ajax({ url: "/api/applications?id=" + applicationId, type: "DELETE" });
    }

    $scope.onApplicationAdded = function (application) {
        $scope.$apply(function () {
            $scope.applications.push(application);
        });
    }

    $scope.onApplicationRemoved = function (application) {
        $scope.$apply(function () {
            for (var i = 0; i < $scope.applications.length; i++) {
                if ($scope.applications[i].applicationId === application.applicationId) {
                    $scope.applications.splice(i, 1);
                }
            }
        });
    },

    $scope.onApplicationUpdated = function () {
        toastService.toast("Successfully Updated!", "OK", "bottom right");
    },

    $scope.onApplicationErrorEncountered = function (error) {
        toastService.toast(error.message, "OK", "bottom right");
    }

    eventService.onApplicationAdded($scope, $scope.onApplicationAdded);
    eventService.onApplicationRemoved($scope, $scope.onApplicationRemoved);
    eventService.onApplicationErrorEncountered($scope, $scope.onApplicationErrorEncountered);
}