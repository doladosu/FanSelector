var gulp = require('gulp');
var uglify = require('gulp-uglify');
var concat = require('gulp-concat');

gulp.task('minify', function() {
  return gulp.src(['wwwroot/app/fanSelectorApp/directives/*.js','wwwroot/app/fanSelectorApp/filters/*.js',
                   'wwwroot/app/fanSelectorApp/*.js', 'wwwroot/app/fanSelectorApp/services/*.js',
                   'wwwroot/app/fanSelectorApp/animations/*.js', 'wwwroot/app/fanSelectorApp/controllers/account/*.js',
                   'wwwroot/app/fanSelectorApp/controllers/contests/*.js', 'wwwroot/app/fanSelectorApp/controllers/*.js'])
    .pipe(uglify())
    .pipe(concat('fanselector.js'))
    .pipe(gulp.dest('wwwroot/lib/_app'));
});


//'wwwroot/lib/angular/angular.min.js', 'wwwroot/lib/angular-route/angular-route.min.js',
//                   'wwwroot/lib/angular-animate/angular-animate.min.js', 'wwwroot/lib/angular-ui-bootstrap-bower/ui-bootstrap.min.js',
//                   'wwwroot/lib/greensock/src/minified/tweenmax.min.js', 'wwwroot/lib/angular-ui-bootstrap-bower/ui-bootstrap-tpls.min.js',
//                   'wwwroot/lib/angular-local-storage/dist/angular-local-storage.min.js',