/*
 * grunt-contrib-uglify
 * http://gruntjs.com/
 *
 * Copyright (c) 2014 "Cowboy" Ben Alman, contributors
 * Licensed under the MIT license.
 */

'use strict';

module.exports = function(grunt) {

  // Project configuration.
  grunt.initConfig({
    uglify: {
      compress: {
        files: {
          'dest/output.min.js': ['MoveEstimator/Scripts/jquery.unobtrusive-ajax.js', 'MoveEstimator/Scripts/jquery.validate.js']
        }
      }
    },
    csslint: {
      strict: {
        options: {
          import: 2
        },
        src: ['MoveEstimator/Content/Site.css']
      }
    }
  });

  grunt.loadNpmTasks('grunt-contrib-uglify');

  grunt.loadNpmTasks('grunt-contrib-csslint');

  // By default, lint and run all tests.
  grunt.registerTask('default', ['uglify:compress', 'csslint:strict']);

};
