/// <reference path="../../../src/Lemonade.Web/scripts/handlers/application.js"/>

QUnit.test("WhenIAddAnApplication_ThenTheApplicationIsAddedToTheList", function (assert) {
    var scope = { applications: [], features: [], $apply: function (func) { func(); } }
    var application = new Application(scope);

    application.addApplication({ application: { applicationId: 1, name: "MyTestApplication" }});
    assert.ok(scope.applications[0].name === "MyTestApplication", "Passed!");
});

QUnit.test("WhenIRemoveAnApplication_ThenTheApplicationIsRemovedFromTheList", function (assert) {
    var scope = { applications: [], features: [], $apply: function (func) { func(); } }
    var application = new Application(scope);

    application.addApplication({ application: { applicationId: 1, name: "MyTestApplication" }});
    application.removeApplication({ application: { applicationId: 1, name: "MyTestApplication" }});
    assert.ok(scope.applications.length === 0, "Passed!");
});