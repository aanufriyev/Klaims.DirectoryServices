var gulp = require("gulp"),
    bower = require("gulp-bower");

gulp.task("bower:install", function() {
	bower({ cmd: "install", production: true })
		.pipe(gulp.dest("wwwroot/lib/"));
});

// For now will just copy to output
gulp.task("scripts", function() {
	gulp.src("App/**/*.*")
		.pipe(gulp.dest("wwwroot/app/"));
});

gulp.task('watch', function() {
  gulp.watch("App/**/*.*", ['scripts']);
  
});

gulp.task("default", ["watch","bower:install"]);