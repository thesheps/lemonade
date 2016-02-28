/// <reference path="../Mocks/EventService.js" />
/// <reference path="../Mocks/Scope.js" />
/// <reference path="../Mocks/Http.js" />
/// <reference path="../../../src/Lemonade.Web/scripts/controllers/application.controller.js"/>

QUnit.test("WhenIAddAnApplication_ThenTheApplicationIsAddedToTheList", function (assert) {
    var scope = new Scope();
    var http = new Http([]);
    var eventService = new EventService();

    applicationController(scope, http, eventService);
    scope.onApplicationAdded({ applicationId: 1, name: "MyTestApplication" });
    assert.ok(scope.applications[0].name === "MyTestApplication", "Passed!");
});

QUnit.test("WhenIRemoveAnApplication_ThenTheApplicationIsRemovedFromTheList", function (assert) {
    var scope = new Scope();
    var http = new Http([]);
    var eventService = new EventService();

    applicationController(scope, http, eventService);
    scope.onApplicationAdded({ applicationId: 1, name: "MyTestApplication" });
    scope.onApplicationRemoved({ applicationId: 1, name: "MyTestApplication" });
    assert.ok(scope.applications.length === 0, "Passed!");
});