var app = angular.module("lemonade", ["ngAnimate", "ngMaterial", "ngRoute"]);

app.controller("aboutController", [function () { }]);
app.controller("applicationController", ["$scope", "$http", "eventService", "toastService", applicationController]);
app.controller("configurationController", ["$scope", "$http", "eventService", "toastService", configurationController]);
app.controller("featureController", ["$scope", "$http", "eventService", "toastService", featureController]);
app.controller("resourceController", ["$scope", "$http", "$mdDialog", "eventService", "toastService", resourceController]);
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
        .when("/Resources", {
            templateUrl: "/views/resources.html",
            controller: "resourceController"
        })
        .when("/About", {
            templateUrl: "/views/about.html",
            controller: "aboutController"
        });

    $mdThemingProvider
        .definePalette("green", {
            '50': "#bde383",
            '100': "#b2df6e",
            '200': "#a7da59",
            '300': "#9cd644",
            '400': "#91d130",
            '500': "#83bd2a",
            '600': "#75a825",
            '700': "#669321",
            '800': "#587e1c",
            '900': "#496a17",
            'A100': "#c8e898",
            'A200': "#d3edad",
            'A400': "#dff1c2",
            'A700': "#3b5513",
            'contrastDefaultColor': "light"
        });

    $mdThemingProvider.theme("default")
        .primaryPalette("green");
});

app.run(function ($rootScope) {
    $rootScope.title = "Lemonade";
});