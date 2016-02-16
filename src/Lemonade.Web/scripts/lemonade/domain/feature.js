function Feature($scope) {
    return {
        addFeature: function (message) {
            $scope.$apply(function () {
                $scope.features.push(message.feature);
            });
        },
        removeFeature: function (message) {
            $scope.$apply(function () {
                for (var i = 0; i < $scope.features.length; i++) {
                    if ($scope.features[i].featureId === message.feature.featureId) {
                        $scope.features.splice(i, 1);
                        return;
                    }
                }
            });
        },
        addFeatureOverride: function (message) {
            $scope.$apply(function () {
                for (var i = 0; i < $scope.features.length; i++) {
                    if ($scope.features[i].featureId === message.featureOverride.featureId) {
                        $scope.features[i].featureOverrides.push(message.featureOverride);
                        return;
                    }
                }
            });
        },
        removeFeatureOverride: function (message) {
            $scope.$apply(function () {
                for (var x = 0; x < $scope.features.length; x++) {
                    var feature = $scope.features[x];

                    for (var y = 0; y < feature.featureOverrides.length; y++) {
                        if (feature.featureOverrides[y].featureOverrideId === message.featureOverride.featureOverrideId) {
                            feature.featureOverrides.splice(y, 1);
                            return;
                        }
                    }
                }
            });
        },
        logError: function (message) {
            $.bootstrapGrowl(message.error.errorMessage, { type: "danger" });
        }
    }
};