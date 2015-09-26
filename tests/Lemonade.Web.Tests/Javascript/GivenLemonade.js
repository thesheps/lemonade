/// <reference path="../../../src/Lemonade.Web/Scripts/Lemonade.js"/>

QUnit.test("WhenIAddAnApplication_ThenTheApplicationIsAddedToTheList", function (assert) {
    var applications = [];

    var view = {
        applications: {
            add: function(application) {
                applications.push(application);
            }
        }
    }

    var lemonade = new Lemonade(view);
    lemonade.addApplication({ ApplicationId: 1, Name: "MyTestApplication" });

    assert.ok(applications[0].Name === "MyTestApplication", "Passed!");
});

QUnit.test("WhenIRemoveAnApplication_ThenTheApplicationIsRemovedFromTheList", function (assert) {
    var applications = [];

    var view = {
        applications: {
            add: function(application) {
                applications.push(application);
            },
            remove: function (application) {
                for (var i = 0; i < applications.length; i++) {
                    if (applications[i].ApplicationId === application.ApplicationId) {
                        applications.splice(i, 1);
                        break;
                    }
                }
            }
        }
    }

    var lemonade = new Lemonade(view);
    lemonade.addApplication({ ApplicationId: 1, Name: "MyTestApplication1" });
    lemonade.removeApplication({ ApplicationId: 1, Name: "MyTestApplication1" });

    assert.ok(applications.length === 0, "Passed!");
});