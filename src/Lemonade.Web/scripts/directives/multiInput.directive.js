angular.module("lemonade")
    .directive("multiInput", function () {
        return {
            restrict: "E",
            templateUrl: "/views/directives/multiInput.html",
            scope: {
                ngModel: "="
            },
            link: function (scope, elem, attrs) {
                scope.$watch("dateValue", function (newValue) { scope.ngModel = newValue; });
                scope.$watch("integerValue", function (newValue) { scope.ngModel = newValue; });
                scope.$watch("decimalValue", function (newValue) { scope.ngModel = newValue; });
                scope.$watch("stringValue", function (newValue) { scope.ngModel = newValue; });
                scope.$watch("booleanValue", function (newValue) { scope.ngModel = newValue; });
                scope.$watch("type", function (newValue) {
                    if (newValue === "Date") { scope.dateValue = null; }
                    if (newValue === "Integer") { scope.integerValue = null; }
                    if (newValue === "Decimal") { scope.decimalValue = null; }
                    if (newValue === "String") { scope.stringValue = null; }
                    if (newValue === "Boolean") { scope.booleanValue = null; }
                });
            }
        };
    });