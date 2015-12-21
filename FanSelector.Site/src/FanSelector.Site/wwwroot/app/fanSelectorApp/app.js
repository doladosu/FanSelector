(function() {

  var app = angular.module('fanSelectorApp',
  ['ngRoute', 'ngAnimate', 'wc.directives', 'ui.bootstrap', 'LocalStorageModule']);

  app.config(['$routeProvider', function($routeProvider) {
      var viewBase = '/app/fanSelectorApp/views/';

      $routeProvider
        .when('/lobby', {
          controller: 'LobbyController',
          templateUrl: viewBase + 'contests/lobby.html',
          controllerAs: 'vm',
          secure: true
        })
        .when('/draft', {
          controller: 'DraftController',
          templateUrl: viewBase + 'contests/draft.html',
          controllerAs: 'vm',
          secure: true
        })
        .when('/contest', {
          controller: 'ContestController',
          templateUrl: viewBase + 'contests/contest.html',
          controllerAs: 'vm',
          secure: true
        })
        .when('/profile', {
          controller: 'ProfileController',
          templateUrl: viewBase + 'account/profile.html',
          controllerAs: 'vm',
          secure: true
        })
        .when('/transaction', {
          controller: 'TransactionController',
          templateUrl: viewBase + 'account/transaction.html',
          controllerAs: 'vm',
          secure: true
        })
        .when('/customers', {
          controller: 'CustomersController',
          templateUrl: viewBase + 'customers/customers.html',
          controllerAs: 'vm'
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
        .when('/login/:redirect*?', {
          controller: 'LoginController',
          templateUrl: viewBase + 'login.html',
          controllerAs: 'vm'
        })
        .otherwise({ redirectTo: '/customers' });

    }
  ]);

  app.run(['$rootScope', '$location', 'authService',
    function ($rootScope, $location, authService) {
      authService.fillAuthData();
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