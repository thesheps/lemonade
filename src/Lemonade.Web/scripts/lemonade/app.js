var app = angular.module("lemonade", ["ngAnimate", "ngMaterial", "ngNewRouter", "lemonade.featureController"]);

app.value("signalRServer", "");

app.controller("AppController", ["$router", AppController]);

function AppController($router) {
    $router.config([
      { path: '/features', component: 'feature' }
    ]);
}

//app.config(function($routeProvider) {
//    $routeProvider
//        .when("/Applications", {
//            templateUrl: "/Views/Applications.html",
//            controller: "applicationController"
//        })
//        .when("/Features", {
//            templateUrl: "/Views/Features.html",
//            controller: "featureController"
//        })
//        .when("/Configurations", {
//            templateUrl: "/Views/Configurations.html",
//            controller: "configurationController"
//        })
//        .when("/About", {
//            templateUrl: "/Views/About.html",
//            controller: "aboutController"
//        });
//});