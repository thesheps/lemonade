/// <reference path="../Mocks/EventService.js" />
/// <reference path="../Mocks/Scope.js" />
/// <reference path="../Mocks/Http.js" />
/// <reference path="../../../src/Lemonade.Web/scripts/controllers/resource.controller.js"/>

QUnit.test("WhenIAddAResource_ThenTheResourceIsAddedToTheList", function (assert) {
    var scope = new Scope();
    var http = new Http([]);
    var eventService = new EventService();

    resourceController(scope, http, , null, eventService);

    scope.onResourceAdded({ resourceId: 1, resourceKey: "MyTestResource" });
    assert.ok(scope.resources[0].resourceKey === "MyTestResource", "Passed!");
});

QUnit.test("WhenIRemoveAResource_ThenTheResourceIsRemovedFromTheList", function (assert) {
    var scope = new Scope();
    var http = new Http([]);
    var eventService = new EventService();

    resourceController(scope, http, null, eventService);

    scope.onResourceAdded({ resource: { resourceId: 1, resourceKey: "MyTestResource" } });
    scope.onResourceRemoved({ resource: { resourceId: 1, resourceKey: "MyTestResource" } });
    assert.ok(scope.resources.length === 0, "Passed!");
});