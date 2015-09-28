function Lemonade(applications, features) {
    return {
        addApplication: function (application) {
            applications.push(application);
        },
        addFeature: function(feature) {
            features.push(feature);
        },
        removeApplication: function (application) {
            for (var i = 0; i < applications.length; i++) {
                if (applications[i].applicationId === application.applicationId) {
                    applications.splice(i, 1);
                }
            }
        },
        removeFeature: function (feature) {
            for (var i = 0; i < features.length; i++) {
                if (features[i].featureId === feature.featureId) {
                    features.splice(i, 1);
                }
            }
        }
    }
};