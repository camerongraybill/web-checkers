/// <binding AfterBuild='default' Clean='clean' />
/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var del = require('del');
var browserify = require('browserify');
var source = require('vinyl-source-stream');
var tsify = require('tsify');
var tsconfig = require("./tsconfig.json");


var OUTPUT_PATH = 'wwwroot/js';

var paths = {
    scripts: ['scripts/**/*.js', 'scripts/**/*.ts', 'scripts/**/*.map']
};

gulp.task('clean', function () {
    return del([OUTPUT_PATH + "/bundle.js"]);
});

gulp.task('default', function () {
    return browserify({
        basedir: '.',
        debug: true,
        entries: tsconfig.files,
        cache: {},
        packageCache: {}
    }).plugin(tsify, tsconfig.compilerOptions).bundle().pipe(source('bundle.js')).pipe(gulp.dest(OUTPUT_PATH));
});