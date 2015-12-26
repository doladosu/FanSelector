(function() {

  var injectParams = ['$location', '$routeParams', 'authService'];

  var registerController = function($location, $routeParams, authService) {
    var vm = this,
      path = '/lobby';

    vm.email = null;
    vm.password = null;
    vm.confirmPassword = null;
    vm.errorMessage = null;

    vm.register = function() {
      authService.register(vm.email, vm.password, vm.confirmPassword).then(function(status) {
        $location.path(path);
      }, function (error) {
        vm.errorMessage = 'Unable to register';
        return;
      });
    };
  };

  registerController.$inject = injectParams;

  angular.module('fanSelectorApp')
    .controller('RegisterController', registerController);

}());