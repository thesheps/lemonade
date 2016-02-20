function Application($scope) {
    return {
        addApplication: function (application) {
            $scope.$apply(function () {
                $scope.applications.push(application);
            });
        },
        removeApplication: function (application) {
            $scope.$apply(function () {
                for (var i = 0; i < $scope.applications.length; i++) {
                    if ($scope.applications[i].applicationId === application.applicationId) {
                        $scope.applications.splice(i, 1);
                    }
                }

                $scope.application = null;
                $scope.features = [];
            });
        },
        logApplicationError: function (error) {
            $.bootstrapGrowl(error.message, { type: "danger" });
        }
    }
};