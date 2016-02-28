var app = angular.module("lemonade", ["ngAnimate", "ngMaterial", "ngRoute"]);

app.controller("aboutController", [function () { }]);
app.controller("applicationController", ["$scope", "$http", "eventService", "toastService", applicationController]);
app.controller("configurationController", ["$scope", "$http", "eventService", "toastService", configurationController]);
app.controller("featureController", ["$scope", "$http", "eventService", "toastService", featureController]);
app.controller("indexController", ["$scope", "$http", "eventService", indexController]);

app.service("eventService", ["$rootScope", eventService]);
app.service("toastService", ["$mdToast", toastService]);

app.config(function ($routeProvider, $mdThemingProvider) {
    $routeProvider
        .when("/Applications", {
            templateUrl: "/views/applications.html",
            controller: "applicationController"
        })
        .when("/Features", {
            templateUrl: "/views/features.html",
            controller: "featureController"
        })
        .when("/Configurations", {
            templateUrl: "/views/configurations.html",
            controller: "configurationController"
        })
        .when("/About", {
            templateUrl: "/views/about.html",
            controller: "aboutController"
        });
});

app.run(function ($rootScope) {
    $rootScope.title = "Lemonade";
});