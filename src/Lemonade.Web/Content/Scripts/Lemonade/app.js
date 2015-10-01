var app = angular.module("lemonade", ["ngAnimate"], function ($locationProvider) {
    $locationProvider.html5Mode(true);
});

app.value("signalRServer", "");