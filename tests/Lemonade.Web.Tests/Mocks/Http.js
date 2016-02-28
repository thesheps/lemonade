function Http(data) {
    var d = data;

    return {
        get: function () {
            return this;
        },
        post: function(data) {
            return this;
        },
        then: function (func) {
            func({ data: d });
        }
    }
}