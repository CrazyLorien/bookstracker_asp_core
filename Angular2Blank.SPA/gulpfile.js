/// <binding ProjectOpened='task' />
var gulp = require("gulp"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    sass = require("gulp-sass"),
    typescript = require("gulp-typescript"),
    uglify = require("gulp-uglify"),
    sourcemaps = require("gulp-sourcemaps");

var paths = {
    root: "./app"
}

var dev = true;

paths.scss = paths.root + "/**/*.scss";
paths.ts = paths.root + "/**/*.ts";
paths.js = "build/js";
paths.css = "build/css";

paths.siteJs = "site.js";
paths.siteCss = "site.css";
paths.siteMinCss = "site.min.css";
paths.siteMinJs= "site.min.js";

gulp.task('task',
    ['scss', 'scss:watch'],
    function () {
    });

gulp.task('scss',
    function () {
        var scssPipe = gulp.src(paths.scss)
            .pipe(sourcemaps.init())
            .pipe(sass())
            .pipe(sourcemaps.write())
            .pipe(gulp.dest(paths.css))
            .pipe(concat(paths.siteCss))
            .pipe(gulp.dest(paths.css))
            .pipe(cssmin())
            .pipe(concat(paths.siteMinCss))
            .pipe(gulp.dest(paths.css));

        return scssPipe;
    });

gulp.task('scss:watch',
    function() {
        gulp.watch(paths.scss, ['scss']);
    });

//gulp.task('ts',
//    function () {
//        var jsPipe = gulp.src(paths.ts)
//            .pipe(sourcemaps.init())
//            .pipe(typescript())
//            .pipe(sourcemaps.write())
//            .pipe(gulp.dest(paths.js))
//            .pipe(concat(paths.siteJs))
//            .pipe(gulp.dest(paths.js))
//            .pipe(concat(paths.siteMinJs))
//            .pipe(uglify())
//            .pipe(gulp.dest(paths.js));

//        return jsPipe;
//    });

//gulp.task('ts:watch',
//    function () {
//        gulp.watch(paths.ts, ['ts']);
//    });

