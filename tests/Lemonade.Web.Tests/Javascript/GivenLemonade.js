/// <reference path="../../../src/Lemonade.Web/Content/Scripts/Lemonade/Lemonade.js"/>


QUnit.test("WhenIAddAnApplication_ThenTheApplicationIsAddedToTheList", function (assert) {
    var scope = { applications: [], features: [], $apply: function (func) { func(); } }
    var lemonade = new Lemonade(scope);

    lemonade.addApplication({ applicationId: 1, name: "MyTestApplication" });
    assert.ok(scope.applications[0].name === "MyTestApplication", "Passed!");
});

QUnit.test("WhenIRemoveAnApplication_ThenTheApplicationIsRemovedFromTheList", function (assert) {
    var scope = { applications: [], features: [], $apply: function (func) { func(); } }
    var lemonade = new Lemonade(scope);

    lemonade.addApplication({ applicationId: 1, name: "MyTestApplication" });
    lemonade.removeApplication({ applicationId: 1, name: "MyTestApplication" });
    assert.ok(scope.applications.length === 0, "Passed!");
});

QUnit.test("WhenIAddAFeature_ThenTheFeatureIsAddedToTheList", function (assert) {
    var scope = { applications: [], features: [], $apply: function (func) { func(); } }
    var lemonade = new Lemonade(scope);

    lemonade.addFeature({ featureId: 1, applicationId: 1, name: "MyTestFeature" });
    assert.ok(scope.features[0].name === "MyTestFeature", "Passed!");
});

QUnit.test("WhenIRemoveAFeature_ThenTheFeatureIsRemovedFromTheList", function (assert) {
    var scope = { applications: [], features: [], $apply: function (func) { func(); } }
    var lemonade = new Lemonade(scope);

    lemonade.addFeature({ featureId: 1, applicationId: 1, name: "MyTestFeature" });
    lemonade.removeFeature({ featureId: 1, applicationId: 1, name: "MyTestFeature" });
    assert.ok(scope.features.length === 0, "Passed!");
});