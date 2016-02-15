function Feature(view) {
    return {
        addFeature: function (feature) {
            view.$apply(function () {
                view.features.push(feature);
            });
        },
        removeFeature: function (feature) {
            view.$apply(function () {
                for (var i = 0; i < view.features.length; i++) {
                    if (view.features[i].featureId === feature.featureId) {
                        view.features.splice(i, 1);
                        return;
                    }
                }
            });
        },
        addFeatureOverride: function (featureOverride) {
            view.$apply(function () {
                for (var i = 0; i < view.features.length; i++) {
                    if (view.features[i].featureId === featureOverride.featureId) {
                        view.features[i].featureOverrides.push(featureOverride);
                        return;
                    }
                }
            });
        },
        removeFeatureOverride: function (featureOverride) {
            view.$apply(function () {
                for (var x = 0; x < view.features.length; x++) {
                    var feature = view.features[x];

                    for (var y = 0; y < feature.featureOverrides.length; y++) {
                        if (feature.featureOverrides[y].featureOverrideId === featureOverride.featureOverrideId) {
                            feature.featureOverrides.splice(y, 1);
                            return;
                        }
                    }
                }
            });
        },
        logError: function (error) {
            $.bootstrapGrowl(error.errorMessage, { type: "danger" });
        }
    }
};