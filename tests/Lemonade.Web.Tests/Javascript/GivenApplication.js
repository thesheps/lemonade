/// <reference path="../../../src/Lemonade.Web/scripts/controllers/application.controller.js"/>

QUnit.test("WhenIAddAnApplication_ThenTheApplicationIsAddedToTheList", function (assert) {
    var scope = { applications: [], features: [], $apply: function (func) { func(); } }
    applicationController(scope);

    scope.onApplicationAdded({ application: { applicationId: 1, name: "MyTestApplication" } });
    assert.ok(scope.applications[0].name === "MyTestApplication", "Passed!");
});

QUnit.test("WhenIRemoveAnApplication_ThenTheApplicationIsRemovedFromTheList", function (assert) {
    var scope = { applications: [], features: [], $apply: function (func) { func(); } }
    applicationController(scope);

    application.onApplicationAdded({ application: { applicationId: 1, name: "MyTestApplication" } });
    application.onApplicationRemoved({ application: { applicationId: 1, name: "MyTestApplication" }});
    assert.ok(scope.applications.length === 0, "Passed!");
});