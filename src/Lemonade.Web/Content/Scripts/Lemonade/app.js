var app = angular.module("lemonade", ["ngAnimate", "xeditable"], function ($locationProvider) {
    $locationProvider.html5Mode(true);
});

app.value("signalRServer", "");
app.run(function(editableOptions, editableThemes) {
    editableOptions.theme = 'bs3'; 
    editableThemes.bs3.inputClass = 'input-sm';
    editableThemes.bs3.buttonsClass = 'btn-xs';
});