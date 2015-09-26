function Lemonade(view) {
    this.view = view;

    return {
        addApplication: function (application) {
            view.applications.add(application);
        },
        removeApplication: function (application) {
            view.applications.remove(application);
        }
    }
};