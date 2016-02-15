function Application(view) {
    return {
        addApplication: function (application) {
            view.$apply(function () {
                view.applications.push(application);
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
        logError: function (error) {
            $.bootstrapGrowl(error.errorMessage, { type: "danger" });
        }
    }
};