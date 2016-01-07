(function() {

  var app = angular.module('fanSelectorApp',
  ['ngRoute', 'ngAnimate', 'wc.directives', 'ui.bootstrap', 'LocalStorageModule']);

  app.config([
    '$routeProvider', function($routeProvider) {
      var viewBase = '/app/fanSelectorApp/views/';

      $routeProvider
        .when('/lobby', {
          controller: 'LobbyController',
          templateUrl: viewBase + 'contests/lobby.html',
          controllerAs: 'vm',
          secure: true
        })
        .when('/contests/:id', {
          controller: 'EnterContestController',
          templateUrl: viewBase + 'contests/enterContest.html',
          controllerAs: 'vm',
          secure: true
        })
        .when('/customerorders/:customerId', {
          controller: 'CustomerOrdersController',
          templateUrl: viewBase + 'customers/customerOrders.html',
          controllerAs: 'vm'
        })
        .when('/customeredit/:customerId', {
          controller: 'CustomerEditController',
          templateUrl: viewBase + 'customers/customerEdit.html',
          controllerAs: 'vm',
          secure: true //This route requires an authenticated user
        })
        .when('/orders', {
          controller: 'OrdersController',
          templateUrl: viewBase + 'orders/orders.html',
          controllerAs: 'vm'
        })
        .when('/about', {
          controller: 'AboutController',
          templateUrl: viewBase + 'about.html',
          controllerAs: 'vm'
        })
        .when('/rules', {
              controller: 'RulesController',
              templateUrl: viewBase + 'rules.html',
              controllerAs: 'vm'
        })
        .when('/howitworks', {
          controller: 'HowController',
          templateUrl: viewBase + 'howitworks.html',
          controllerAs: 'vm'
        })
        .when('/register', {
          controller: 'RegisterController',
          templateUrl: viewBase + 'register.html',
          controllerAs: 'vm'
        })
        .when('/login/:redirect*?', {
          controller: 'LoginController',
          templateUrl: viewBase + 'login.html',
          controllerAs: 'vm'
        })
        .otherwise({ redirectTo: '/lobby' });
    }
  ]);

  app.run([
    '$rootScope', '$location', 'authService',
    function($rootScope, $location, authService) {
      authService.fillAuthData();
      //Client-side security. Server-side framework MUST add it's 
      //own security as well since client-based security is easily hacked
      $rootScope.$on("$routeChangeStart", function(event, next, current) {
        if (next && next.$$route && next.$$route.secure) {
          if (!authService.user.isAuthenticated) {
            $rootScope.$evalAsync(function() {
              authService.redirectToLogin();
            });
          }
        }
      });

    }
  ]);

}());