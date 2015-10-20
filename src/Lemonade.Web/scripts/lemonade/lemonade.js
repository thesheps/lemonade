function Lemonade(view) {
    return {
        addApplication: function (application) {
            view.$apply(function () {
                view.applications.push(application);
            });
        },
        addFeature: function (feature) {
            view.$apply(function () {
                view.features.push(feature);
            });
        },
        removeApplication: function (application) {
            view.$apply(function () {
                for (var i = 0; i < view.applications.length; i++) {
                    if (view.applications[i].applicationId === application.applicationId) {
                        view.applications.splice(i, 1);
                    }
                }

                view.application = null;
                view.features = [];
            });
        },
        removeFeature: function (feature) {
            view.$apply(function () {
                for (var i = 0; i < view.features.length; i++) {
                    if (view.features[i].featureId === feature.featureId) {
                        view.features.splice(i, 1);
                    }
                }
            });
        },
        logError: function (error) {
            $.bootstrapGrowl(error.errorMessage, { type: "danger" });
        }
    }
};