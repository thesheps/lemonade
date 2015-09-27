var app = angular.module("lemonade", [], function ($locationProvider) {
    $locationProvider.html5Mode(true);
});

app.value("signalRServer", "");