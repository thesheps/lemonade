var app = angular.module("lemonade", ["ngAnimate", "xeditable", "ngRoute"]);

app.value("signalRServer", "");

app.run(function(editableOptions, editableThemes) {
    editableOptions.theme = 'bs3'; 
    editableThemes.bs3.inputClass = 'input-sm';
    editableThemes.bs3.buttonsClass = 'btn-xs';
});

app.config(function($routeProvider) {
    $routeProvider
        .when("/Features", {
            templateUrl: "/Views/Features.html",
            controller: "featureController"
        })
        .when("/About", {
            templateUrl: "/Views/About.html",
            controller: "aboutController"
        });
})