// Theme overrides
app.config(function ($mdThemingProvider) {
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

// Constant overrides
app.run(function ($rootScope) {
    $rootScope.title = "Configuration Management";
});