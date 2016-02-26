var gulp = require("gulp");
var msbuild = require("gulp-msbuild");
var watch = require('gulp-watch');
var browserSync = require('browser-sync').create();
 
gulp.task("watch", function () {    
    gulp.watch([
        "src/Lemonade.Web/scripts/**/*.js",
        "src/Lemonade.Web/views/**/*.html"
    ], ["msbuild", browserSync.reload]);
});

gulp.task("msbuild", function() {
	return gulp.src("./Lemonade.sln")
		.pipe(msbuild({
			targets: ['Clean', 'Build'],
			stdout: false,
            configuration: 'Debug',
            toolsVersion: 14.0,
			}));
});

gulp.task('browser-sync', function() {
    browserSync.init({
        proxy: "dev-lemonade"
    });
});

gulp.task("default", ["browser-sync", "watch"]);