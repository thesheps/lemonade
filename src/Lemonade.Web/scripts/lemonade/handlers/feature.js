function Feature($scope) {
    return {
        addFeature: function (feature) {
            $scope.$apply(function () {
                $scope.features.push(feature);
            });
        },
        removeFeature: function (feature) {
            $scope.$apply(function () {
                for (var i = 0; i < $scope.features.length; i++) {
                    if ($scope.features[i].featureId === feature.featureId) {
                        $scope.features.splice(i, 1);
                        return;
                    }
                }
            });
        },
        addFeatureOverride: function (featureOverride) {
            $scope.$apply(function () {
                for (var i = 0; i < $scope.features.length; i++) {
                    if ($scope.features[i].featureId === featureOverride.featureId) {
                        $scope.features[i].featureOverrides.push(featureOverride);
                        return;
                    }
                }
            });
        },
        removeFeatureOverride: function (featureOverride) {
            $scope.$apply(function () {
                for (var x = 0; x < $scope.features.length; x++) {
                    var feature = $scope.features[x];

                    for (var y = 0; y < feature.featureOverrides.length; y++) {
                        if (feature.featureOverrides[y].featureOverrideId === featureOverride.featureOverrideId) {
                            feature.featureOverrides.splice(y, 1);
                            return;
                        }
                    }
                }
            });
        },
        logFeatureError: function (error) {
            $.bootstrapGrowl(error.message, { type: "danger" });
        }
    }
};