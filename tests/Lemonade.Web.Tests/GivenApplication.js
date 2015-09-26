/// <reference path="../../src/Lemonade.Web/Scripts/Application.js"/>

QUnit.test("hello test", function (assert) {
    assert.ok(Application.hello() === 123, "Passed!");    
});