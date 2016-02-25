angular.module("lemonade")
    .service("toastService", ["$mdToast", toastService]);

function toastService($mdToast) {
    return {
        toast: function(title, buttonText, position) {
            $mdToast.show($mdToast.simple()
                .content(title)
                .action(buttonText)
                .position(position));
        }
    }
}