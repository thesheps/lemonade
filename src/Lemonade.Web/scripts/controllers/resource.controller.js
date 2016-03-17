function resourceController($scope, $http, $mdDialog, eventService, toastService) {
    $http.get("/api/applications").then(function (res) {
        $scope.applications = res.data;
    });

    $http.get("/api/locales").then(function (res) {
        $scope.locales = res.data;
    });

    $scope.selectApplication = function (application) {
        $http.get("/api/resources?applicationId=" + application.applicationId).then(function (res) {
            $scope.application = application;
            $scope.newResource = { applicationId: application.applicationId }
            $scope.resources = res.data;
        });
    }

    $scope.showCreateDialog = function (resource) {
        $mdDialog.show({
            controller: function ($mdDialog, $http, $scope) {
                $scope.newResource = { applicationId: resource.applicationId, localeId: resource.localeId };

                $scope.addResource = function (resource) {
                    $http.post("/api/resources", resource);
                    $mdDialog.cancel();
                }

                $scope.cancelDialog = function () {
                    $mdDialog.cancel();
                }
            },
            templateUrl: "views/dialogs/create-resource-dialog.html",
            parent: angular.element(document.body),
            clickOutsideToClose: true,
            fullscreen: false
        });
    }

    $scope.showFilterDialog = function (criteria) {
        $mdDialog.show({
            controller: function ($scope) {
                $scope.applyFilter = function (filter) {
                    criteria.resourceSet = filter ? filter.resourceSet : "";
                    criteria.resourceKey = filter ? filter.resourceKey : "";
                    criteria.value = filter ? filter.value : "";
                    $mdDialog.cancel();
                }

                $scope.cancelDialog = function () {
                    $mdDialog.cancel();
                }
            },
            templateUrl: "views/dialogs/filter-resource-dialog.html",
            parent: angular.element(document.body),
            clickOutsideToClose: true,
            fullscreen: false
        });
    }

    $scope.showGenerateResourcesDialog = function(applicationId, localeId, locales) {
        $mdDialog.show({
            controller: function ($scope, $http) {
                $scope.locales = locales;

                $scope.generateResources = function () {
                    $http.post("/api/resources/generate", {
                        applicationId : applicationId,
                        localeId: localeId,
                        targetLocaleId: $scope.targetLocaleId,
                        translationType: $scope.translationType
                    });

                    $mdDialog.cancel();
                }

                $scope.cancelDialog = function () {
                    $mdDialog.cancel();
                }
            },
            templateUrl: "views/dialogs/generate-resources-dialog.html",
            parent: angular.element(document.body),
            clickOutsideToClose: true,
            fullscreen: false
        });
    }

    $scope.updateResource = function (resource) {
        $http.put("/api/resources", resource);
    }

    $scope.deleteResource = function (resourceId) {
        $.ajax({ url: "/api/resources?id=" + resourceId, type: "DELETE" });
    }

    $scope.resourceFilter = function (criteria) {
        return function (resource) {
            return (!angular.isDefined(criteria)) ||
                   (!angular.isDefined(criteria.locale) || criteria.locale === "Show all..." || resource.localeId === criteria.locale) &&
                   (!angular.isDefined(criteria.resourceSet) || criteria.resourceSet === "" || resource.resourceSet.indexOf(criteria.resourceSet) >= 0) &&
                   (!angular.isDefined(criteria.resourceKey) || criteria.resourceKey === "" || resource.resourceKey.indexOf(criteria.resourceKey) >= 0) &&
                   (!angular.isDefined(criteria.value) || criteria.value === "" || resource.value.indexOf(criteria.value) >= 0);
        };
    }

    $scope.usedLocales = function (locale) {
        if (!$scope.resources) {
            return false;
        }

        for (var i = 0; i < $scope.resources.length; i++) {
            if ($scope.resources[i].localeId === locale.localeId) {
                return true;
            }
        }

        return false;
    }

    $scope.onResourceAdded = function (resource) {
        $scope.$apply(function () {
            $scope.resources.push(resource);
        });
    }

    $scope.onResourceRemoved = function (resource) {
        $scope.$apply(function () {
            for (var i = 0; i < $scope.resources.length; i++) {
                if ($scope.resources[i].resourceId === resource.resourceId) {
                    $scope.resources.splice(i, 1);
                    return;
                }
            }
        });
    }

    $scope.onResourceUpdated = function (resource) {
        toastService.toast("Successfully updated!", "OK", "bottom right");
    }

    $scope.onResourcesGenerated = function (resource) {
        toastService.toast("Successfully generated!", "OK", "bottom right");
    }

    $scope.onResourceErrorEncountered = function (error) {
        toastService.toast(error.message, "OK", "bottom right");
    }

    eventService.onResourceAdded($scope, $scope.onResourceAdded);
    eventService.onResourceRemoved($scope, $scope.onResourceRemoved);
    eventService.onResourceUpdated($scope, $scope.onResourceUpdated);
    eventService.onResourceErrorEncountered($scope, $scope.onResourceErrorEncountered);
    eventService.onResourcesGenerated($scope, $scope.onResourcesGenerated);
}