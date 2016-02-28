function Scope(a,c,f,fo) {
    return {
        configurations: c ? c : [],
        applications: a ? a : [],
        features: f ? f : [],
        featureOverrides: fo ? fo : [],
        $apply: function(func) {
            func();
        }
    }
}