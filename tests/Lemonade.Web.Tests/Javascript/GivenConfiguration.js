/// <reference path="../../../src/Lemonade.Web/scripts/lemonade/handlers/configuration.js"/>

QUnit.test("WhenIAddAConfiguration_ThenTheConfigurationIsAddedToTheList", function (assert) {
    var scope = { configurations: [], $apply: function (func) { func(); } }
    var configuration = new Configuration(scope);

    configuration.addConfiguration({ configuration: { configurationId: 1, name: "MyTestConfiguration" } });
    assert.ok(scope.configurations[0].name === "MyTestConfiguration", "Passed!");
});

QUnit.test("WhenIRemoveAConfiguration_ThenTheConfigurationIsRemovedFromTheList", function (assert) {
    var scope = { configurations: [], $apply: function (func) { func(); } }
    var configuration = new Configuration(scope);

    configuration.addConfiguration({ configuration: { configurationId: 1, name: "MyTestConfiguration" } });
    configuration.removeConfiguration({ configuration: { configurationId: 1, name: "MyTestConfiguration" } });
    assert.ok(scope.configurations.length === 0, "Passed!");
});