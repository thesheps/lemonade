/// <reference path="../../../src/Lemonade.Web/scripts/handlers/feature.js"/>

QUnit.test("WhenIAddAFeature_ThenTheFeatureIsAddedToTheList", function (assert) {
    var scope = { applications: [], features: [], $apply: function (func) { func(); } }
    var feature = new Feature(scope);

    feature.addFeature({ feature: { featureId: 1, applicationId: 1, name: "MyTestFeature" } });
    assert.ok(scope.features[0].name === "MyTestFeature", "Passed!");
});

QUnit.test("WhenIRemoveAFeature_ThenTheFeatureIsRemovedFromTheList", function (assert) {
    var scope = { applications: [], features: [], $apply: function (func) { func(); } }
    var feature = new Feature(scope);

    feature.addFeature({ feature: { featureId: 1, applicationId: 1, name: "MyTestFeature" } });
    feature.removeFeature({ feature: { featureId: 1, applicationId: 1, name: "MyTestFeature" } });
    assert.ok(scope.features.length === 0, "Passed!");
});

QUnit.test("WhenIAddAFeatureOverride_ThenTheFeatureOverrideIsAddedToTheList", function (assert) {
    var scope = { applications: [], features: [{ featureId: 1, applicationId: 1, name: "MyTestFeature", featureOverrides: [] }], $apply: function (func) { func(); } }
    var feature = new Feature(scope);

    feature.addFeatureOverride({ featureOverride: { featureId: 1, featureOverrideId: 1, hostname: "TestHostname" } });
    assert.ok(scope.features[0].featureOverrides[0].hostname === "TestHostname", "Passed!");
});

QUnit.test("WhenIAddAFeatureOverrideForANewFeature_ThenTheFeatureOverrideIsAddedToTheList", function (assert) {
    var scope = { applications: [], features: [{ featureId: 1, applicationId: 1, name: "MyTestFeature", featureOverrides: [] }], $apply: function (func) { func(); } }
    var feature = new Feature(scope);

    feature.addFeatureOverride({ featureOverride: { featureId: 1, featureOverrideId: 1, hostname: "TestHostname" } });
    assert.ok(scope.features[0].featureOverrides[0].hostname === "TestHostname", "Passed!");
});

QUnit.test("WhenIRemoveAFeatureOverride_ThenTheFeatureOverrideIsRemovedFromTheList", function (assert) {
    var scope = { applications: [], features: [], $apply: function (func) { func(); } }
    var feature = new Feature(scope);

    feature.addFeature({
        feature: {
            featureId: 1, applicationId: 1, name: "MyTestFeature", featureOverrides: [
                { featureOverrideId: 1 }
            ]
        }
    });

    feature.removeFeatureOverride({ featureOverride: { featureOverrideId: 1 } });
    assert.ok(scope.features[0].featureOverrides.length === 0, "Passed!");
});