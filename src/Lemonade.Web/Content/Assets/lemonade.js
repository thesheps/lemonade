function Lemonade(view) {
    return {
        addApplication: function (application) {
            view.$apply(function () {
                view.applications.push(application);
            });
        },
        addFeature: function (feature) {
            view.$apply(function () {
                view.features.push(feature);
                view.newFeature = null;
            });
        },
        removeApplication: function (application) {
            view.$apply(function () {
                for (var i = 0; i < view.applications.length; i++) {
                    if (view.applications[i].applicationId === application.applicationId) {
                        view.applications.splice(i, 1);
                    }
                }

                view.application = null;
                view.features = [];
            });
        },
        removeFeature: function (feature) {
            view.$apply(function () {
                for (var i = 0; i < view.features.length; i++) {
                    if (view.features[i].featureId === feature.featureId) {
                        view.features.splice(i, 1);
                    }
                }
            });
        },
        logError: function (error) {
            $.bootstrapGrowl(error.errorMessage, { type: "danger" });
        }
    }
};

if (typeof angular != 'undefined') {
    var app = angular.module("lemonade", ["ngAnimate", "xeditable"], function ($locationProvider) {
        $locationProvider.html5Mode(true);
    });

    app.value("signalRServer", "");
    app.run(function (editableOptions, editableThemes) {
        editableOptions.theme = 'bs3';
        editableThemes.bs3.inputClass = 'input-sm';
        editableThemes.bs3.buttonsClass = 'btn-xs';
    });

    angular.module("lemonade")
        .controller("lemonadeController", ["$scope", "$http", function ($scope, $http) {
            $http.get("/api/applications").then(function (res) {
                $scope.applications = res.data;
                $.connection.lemonadeHub.client = new Lemonade($scope);
                $.connection.hub.start();
            });

            $scope.selectApplication = function (application) {
                $http.get("/api/features?applicationId=" + application.applicationId).then(function (res) {
                    $scope.application = application;
                    $scope.newFeature = { applicationId: application.applicationId }
                    $scope.features = res.data;
                });
            }

            $scope.addApplication = function (applicationName) {
                $.post("api/applications", { name: applicationName });
            }

            $scope.updateApplication = function (application) {
                $http.put("/api/applications", application);
            }

            $scope.deleteApplication = function (applicationId) {
                $.ajax({ url: 'api/applications?id=' + applicationId, type: 'DELETE' });
            }

            $scope.addFeature = function (feature) {
                $http.post("/api/features", feature);
            }

            $scope.updateFeature = function (feature) {
                $http.put("/api/features", feature);
            }

            $scope.deleteFeature = function (featureId) {
                $.ajax({ url: 'api/features?id=' + featureId, type: 'DELETE' });
            }
        }]);
}