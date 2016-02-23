var app = angular.module("lemonade", ["ngAnimate", "ngMaterial", "ngRoute"]);

app.value("signalRServer", "");

app.config(function($routeProvider) {
    $routeProvider
        .when("/Applications", {
            templateUrl: "/Views/Applications.html",
            controller: "applicationController"
        })
        .when("/Features", {
            templateUrl: "/Views/Features.html",
            controller: "featureController"
        })
        .when("/Configurations", {
            templateUrl: "/Views/Configurations.html",
            controller: "configurationController"
        })
        .when("/About", {
            templateUrl: "/Views/About.html",
            controller: "aboutController"
        });
});