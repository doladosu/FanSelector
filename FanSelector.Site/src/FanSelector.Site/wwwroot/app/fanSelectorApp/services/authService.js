(function() {

  var injectParams = ['$http', '$rootScope', '$q', 'localStorageService', 'config'];

  var authFactory = function($http, $rootScope, $q, localStorageService, config) {
    var serviceBase = '/api/dataservice/',
        factory = {
          loginPath: '/login',
          user: {
            isAuthenticated: false,
            roles: null,
            email: ''
          }
        };

    factory.login = function(email, password) {
      var data = "grant_type=password&username=" + email + "&password=" + password + "&client_id=" + config.clientId;

      var deferred = $q.defer();

      $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function(response) {

        localStorageService.set('authorizationData', { token: response.access_token, email: email, refreshToken: response.refresh_token, useRefreshTokens: true });
        var loggedIn = results.data.status;
        changeAuth(loggedIn);

        user.isAuthenticated = true;
        user.email = email;

        deferred.resolve(response);

      }).error(function(err, status) {
        logout();
        deferred.reject(err);
      });

      return deferred.promise;
    };

    factory.logout = function() {
      localStorageService.remove('authorizationData');
      user.email = '';
      changeAuth(false);
    };

    factory.redirectToLogin = function() {
      $rootScope.$broadcast('redirectToLogin', null);
    };

    factory.fillAuthData = function() {
      var authData = localStorageService.get('authorizationData');
      if (authData) {
        user.isAuthenticated = true;
        user.email = authData.email;
      }
    };

    function changeAuth(loggedIn) {
      factory.user.isAuthenticated = loggedIn;
      $rootScope.$broadcast('loginStatusChanged', loggedIn);
    }

    return factory;
  };

  authFactory.$inject = injectParams;

  angular.module('fanSelectorApp').factory('authService', authFactory);

}());