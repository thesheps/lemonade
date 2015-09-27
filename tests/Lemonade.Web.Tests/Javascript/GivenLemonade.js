/// <reference path="../../../src/Lemonade.Web/Scripts/Lemonade/Lemonade.js"/>

QUnit.test("WhenIAddAnApplication_ThenTheApplicationIsAddedToTheList", function (assert) {
    var applications = [];
    var lemonade = new Lemonade(applications);

    lemonade.addApplication({ applicationId: 1, name: "MyTestApplication" });
    assert.ok(applications[0].name === "MyTestApplication", "Passed!");
});

QUnit.test("WhenIRemoveAnApplication_ThenTheApplicationIsRemovedFromTheList", function (assert) {
    var applications = [];
    var lemonade = new Lemonade(applications);

    lemonade.addApplication({ applicationId: 1, name: "MyTestApplication" });
    lemonade.removeApplication({ applicationId: 1, name: "MyTestApplication" });
    assert.ok(applications.length === 0, "Passed!");
});

QUnit.test("WhenIAddAFeature_ThenTheFeatureIsAddedToTheList", function (assert) {
    var features = [];
    var lemonade = new Lemonade([], features);

    lemonade.addFeature({ featureId: 1, applicationId: 1, name: "MyTestFeature" });
    assert.ok(features[0].name === "MyTestFeature", "Passed!");
});

QUnit.test("WhenIRemoveAFeature_ThenTheFeatureIsRemovedFromTheList", function (assert) {
    var features = [];
    var lemonade = new Lemonade([], features);

    lemonade.addFeature({ featureId: 1, applicationId: 1, name: "MyTestFeature" });
    lemonade.removeFeature({ featureId: 1, applicationId: 1, name: "MyTestFeature" });
    assert.ok(features.length === 0, "Passed!");
});