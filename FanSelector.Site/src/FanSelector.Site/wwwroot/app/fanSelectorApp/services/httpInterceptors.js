(function() {

  angular.module('fanSelectorApp')
    .config([
      '$httpProvider', 'localStorageService', function($httpProvider, localStorageService) {

        var injectParams = ['$q', '$rootScope'];

        var request = function(config) {

          config.headers = config.headers || {};

          var authData = localStorageService.get('authorizationData');
          if (authData) {
            config.headers.Authorization = 'Bearer ' + authData.token;
          }

          return config;
        }

        var httpInterceptor401 = function($q, $rootScope) {

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

        httpInterceptor401.$inject = injectParams;

        $httpProvider.interceptors.push(request);
        $httpProvider.interceptors.push(httpInterceptor401);

      }
    ]);
}());