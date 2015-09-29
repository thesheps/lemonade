function Lemonade(applications, features, $scope) {
    return {
        addApplication: function (application) {
            $scope.$apply(function () {
                applications.push(application);
            });
        },
        addFeature: function (feature) {
            $scope.$apply(function () {
                features.push(feature);
            });
        },
        removeApplication: function (application) {
            $scope.$apply(function () {
                for (var i = 0; i < applications.length; i++) {
                    if (applications[i].applicationId === application.applicationId) {
                        applications.splice(i, 1);
                    }
                }
            });
        },
        removeFeature: function (feature) {
            $scope.$apply(function () {
                for (var i = 0; i < features.length; i++) {
                    if (features[i].featureId === feature.featureId) {
                        features.splice(i, 1);
                    }
                }
            });
        }
    }
};