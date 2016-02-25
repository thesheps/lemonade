angular.module("lemonade")
    .directive("titleBar", function ($mdSidenav) {
        return {
            restrict: 'E',
            templateUrl: "/views/directives/titleBar.html",
            scope: { title: "@" },
            link: function (scope) {
                scope.toggleMenu = function () {
                    $mdSidenav('sideNav').toggle();
                }
            }
        };
    });