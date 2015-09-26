function Lemonade(view) {
    this.view = view;

    return {
        addApplication: function (application) {
            view.applications.add(application);
        },
        removeApplication: function (applicationId) {
            view.applications.remove(applicationId);
        }
    }
};