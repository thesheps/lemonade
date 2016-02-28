/// <reference path="../Mocks/EventService.js" />
/// <reference path="../Mocks/Scope.js" />
/// <reference path="../Mocks/Http.js" />
/// <reference path="../../../src/Lemonade.Web/scripts/controllers/configuration.controller.js"/>

QUnit.test("WhenIAddAConfiguration_ThenTheConfigurationIsAddedToTheList", function (assert) {
    var scope = new Scope();
    var http = new Http([]);
    var eventService = new EventService();

    configurationController(scope, http, eventService);

    scope.onConfigurationAdded({ configurationId: 1, name: "MyTestConfiguration" });
    assert.ok(scope.configurations[0].name === "MyTestConfiguration", "Passed!");
});

QUnit.test("WhenIRemoveAConfiguration_ThenTheConfigurationIsRemovedFromTheList", function (assert) {
    var scope = new Scope();
    var http = new Http([]);
    var eventService = new EventService();

    configurationController(scope, http, eventService);

    scope.onConfigurationAdded({ configuration: { configurationId: 1, name: "MyTestConfiguration" } });
    scope.onConfigurationRemoved({ configuration: { configurationId: 1, name: "MyTestConfiguration" } });
    assert.ok(scope.configurations.length === 0, "Passed!");
});