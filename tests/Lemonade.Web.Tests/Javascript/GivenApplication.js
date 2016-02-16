﻿/// <reference path="../../../src/Lemonade.Web/scripts/lemonade/application.js"/>

QUnit.test("WhenIAddAnApplication_ThenTheApplicationIsAddedToTheList", function (assert) {
    var scope = { applications: [], features: [], $apply: function (func) { func(); } }
    var lemonade = new Application(scope);

    lemonade.addApplication({ applicationId: 1, name: "MyTestApplication" });
    assert.ok(scope.applications[0].name === "MyTestApplication", "Passed!");
});

QUnit.test("WhenIRemoveAnApplication_ThenTheApplicationIsRemovedFromTheList", function (assert) {
    var scope = { applications: [], features: [], $apply: function (func) { func(); } }
    var lemonade = new Application(scope);

    lemonade.addApplication({ applicationId: 1, name: "MyTestApplication" });
    lemonade.removeApplication({ applicationId: 1, name: "MyTestApplication" });
    assert.ok(scope.applications.length === 0, "Passed!");
});