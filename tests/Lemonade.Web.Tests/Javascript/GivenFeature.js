/// <reference path="../Mocks/EventService.js" />
/// <reference path="../Mocks/Scope.js" />
/// <reference path="../Mocks/Http.js" />
/// <reference path="../../../src/Lemonade.Web/scripts/controllers/feature.controller.js"/>

QUnit.test("WhenIAddAFeature_ThenTheFeatureIsAddedToTheList", function (assert) {
    var scope = new Scope();
    var http = new Http([]);
    var eventService = new EventService();

    featureController(scope, http, eventService);

    scope.onFeatureAdded({ featureId: 1, applicationId: 1, name: "MyTestFeature" });
    console.log(scope.features[0].name);
    assert.ok(scope.features[0].name === "MyTestFeature", "Passed!");
});

QUnit.test("WhenIRemoveAFeature_ThenTheFeatureIsRemovedFromTheList", function (assert) {
    var scope = new Scope();
    var http = new Http([]);
    var eventService = new EventService();

    featureController(scope, http, eventService);

    scope.onFeatureAdded({ featureId: 1, applicationId: 1, name: "MyTestFeature" });
    scope.onFeatureRemoved({ featureId: 1, applicationId: 1, name: "MyTestFeature" });
    assert.ok(scope.features.length === 0, "Passed!");
});

QUnit.test("WhenIAddAFeatureOverride_ThenTheFeatureOverrideIsAddedToTheList", function (assert) {
    var scope = new Scope([], [], [{ featureId: 1, applicationId: 1, name: "MyTestFeature", featureOverrides: [] }], []);
    var http = new Http([]);
    var eventService = new EventService();

    featureController(scope, http, eventService);

    scope.onFeatureOverrideAdded({ featureId: 1, featureOverrideId: 1, hostname: "TestHostname" });
    assert.ok(scope.features[0].featureOverrides[0].hostname === "TestHostname", "Passed!");
});

QUnit.test("WhenIRemoveAFeatureOverride_ThenTheFeatureOverrideIsRemovedFromTheList", function (assert) {
    var scope = new Scope();
    var http = new Http([]);
    var eventService = new EventService();

    featureController(scope, http, eventService);

    scope.onFeatureAdded({
        featureId: 1, applicationId: 1, name: "MyTestFeature", featureOverrides: [
            { featureOverrideId: 1 }
        ]
    });

    scope.onFeatureOverrideRemoved({ featureOverrideId: 1 });
    assert.ok(scope.features[0].featureOverrides.length === 0, "Passed!");
});