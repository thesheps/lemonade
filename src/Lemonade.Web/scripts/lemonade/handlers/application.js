function Application($scope) {
    return {
        addApplication: function (message) {
            $scope.$apply(function () {
                $scope.applications.push(message.application);
            });
        },
        removeApplication: function (message) {
            $scope.$apply(function () {
                for (var i = 0; i < $scope.applications.length; i++) {
                    if ($scope.applications[i].applicationId === message.application.applicationId) {
                        $scope.applications.splice(i, 1);
                    }
                }

                $scope.application = null;
                $scope.features = [];
            });
        },
        logError: function (message) {
            $.bootstrapGrowl(message.error.errorMessage, { type: "danger" });
        }
    }
};