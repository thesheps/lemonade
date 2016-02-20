function Configuration($scope) {
    return {
        addConfiguration: function (message) {
            $scope.$apply(function () {
                $scope.configurations.push(message.configuration);
            });
        },
        removeConfiguration: function (message) {
            $scope.$apply(function () {
                for (var i = 0; i < $scope.configurations.length; i++) {
                    if ($scope.configurations[i].configurationId === message.configuration.configurationId) {
                        $scope.configurations.splice(i, 1);
                    }
                }

                $scope.configuration = null;
            });
        },
        logError: function (message) {
            $.bootstrapGrowl(message.error.errorMessage, { type: "danger" });
        }
    }
};