(function() {
  'use strict';

  var injectParams = ['$q', 'localStorageService', '$rootScope'];
  var authInterceptorFactory = function($q, localStorageService, $rootScope) {
    var service = {
    
    };

    service.request = function(config) {

      config.headers = config.headers || {};

      var authData = localStorageService.get('authorizationData');
      if (authData) {
        config.headers.Authorization = 'Bearer ' + authData.token;
      }

      return config;
    }


    service.httpInterceptor401 = function() {

      var success = function(response) {
        return response;
      };

      var error = function(res) {
        if (res.status === 401) {
          //Raise event so listener (navbarController) can act on it
          $rootScope.$broadcast('redirectToLogin', null);
          return $q.reject(res);
        }
        return $q.reject(res);
      };

      return function(promise) {
        return promise.then(success, error);
      };

    };

    return service;
  };

  authInterceptorFactory.$inject = injectParams;
  var module = angular.module('fanSelectorApp');
  module.factory('AuthInterceptor', authInterceptorFactory);
  module.config(['$httpProvider', function($httpProvider) {
      $httpProvider.interceptors.push('AuthInterceptor');
    }
  ]);
})();