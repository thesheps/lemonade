function Configuration($scope) {
    return {
        addConfiguration: function (configuration) {
            $scope.$apply(function () {
                $scope.configurations.push(configuration);
            });
        },
        removeConfiguration: function (configuration) {
            $scope.$apply(function () {
                for (var i = 0; i < $scope.configurations.length; i++) {
                    if ($scope.configurations[i].configurationId === configuration.configurationId) {
                        $scope.configurations.splice(i, 1);
                    }
                }

                $scope.configuration = null;
            });
        },
        logConfigurationError: function (error) {
            $.bootstrapGrowl(error.message, { type: "danger" });
        }
    }
};