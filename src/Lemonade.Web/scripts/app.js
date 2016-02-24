var app = angular.module("lemonade", ["ngAnimate", "ngMaterial", "ngRoute"]);

app.value("signalRServer", "");

app.config(function($routeProvider) {
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