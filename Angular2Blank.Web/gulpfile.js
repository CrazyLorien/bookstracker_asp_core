/// <binding ProjectOpened='task' />
var gulp = require("gulp"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    sass = require("gulp-sass"),
    typescript = require("gulp-typescript"),
    uglify = require("gulp-uglify"),
    sourcemaps = require("gulp-sourcemaps");

var paths = {
    root: "./wwwroot"
}

var dev = true;

paths.scss = paths.root + "/**/*.scss";
paths.ts = paths.root + "/**/*.ts";
paths.js = paths.root + "/build/js";
paths.css = paths.root + "/build/css";

paths.siteJs = "site.js";
paths.siteCss = "site.css";

gulp.task('scss',
    function () {
        var scssPipe = gulp.src(paths.scss)
            .pipe(sourcemaps.init())
            .pipe(sass())
            .pipe(sourcemaps.write())
            .pipe(gulp.dest(paths.css))
            .pipe(concat(paths.siteCss));

        if (dev) {
            scssPipe = scssPipe.pipe(gulp.dest(paths.css));
        } else {
            scssPipe = scssPipe
                .pipe(cssmin())
                .pipe(gulp.dest(paths.css));
        }

        return scssPipe;
    });

gulp.task('ts',
    function () {
        var jsPipe = gulp.src(paths.ts)
            .pipe(sourcemaps.init())
            .pipe(typescript())
            .pipe(sourcemaps.write())
            .pipe(gulp.dest(paths.js))
            .pipe(concat(paths.siteJs));

        if (dev) {
            jsPipe = jsPipe.pipe(gulp.dest(paths.js));
        } else {
            jsPipe = jsPipe
                .pipe(uglify())
                .pipe(gulp.dest(paths.js));
        }

        return jsPipe;
    });

gulp.task('scss:watch',
    function() {
        gulp.watch(paths.scss, ['scss']);
    });

gulp.task('ts:watch',
    function () {
        gulp.watch(paths.ts, ['ts']);
    });

gulp.task('task',
    ['scss', 'ts', 'scss:watch', 'ts:watch'],
    function() {
    });