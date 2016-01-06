(function() {

  var injectParams = [
    '$scope', '$location', '$filter', '$window', 'authService', 'contestsService', 'signalRSvc'
  ];

  var lobbyController = function ($scope, $location, $filter, $window, authService, contestsService, signalRSvc) {

    var vm = this;

    signalRSvc.initialize();

    $scope.$parent.$on("enteredContest", function (e, message) {
      $scope.$apply(function () {
        console.log(message);
      });
    });

    vm.contests = [];
    vm.filteredcontests = [];
    vm.filteredCount = 0;
    vm.orderby = 'contestDate';
    vm.reverse = false;
    vm.searchText = null;
    vm.noContest = false;

    //paging
    vm.totalRecords = 0;
    vm.pageSize = 10;
    vm.currentPage = 1;

    vm.navigate = function(url) {
      $location.path(url);
    };

    vm.setOrder = function(orderby) {
      if (orderby === vm.orderby) {
        vm.reverse = !vm.reverse;
      }
      vm.orderby = orderby;
    };

    function filtercontests(filterText) {
      vm.filteredcontests = $filter("nameCityStateFilter")(vm.contests, filterText);
      vm.filteredCount = vm.filteredcontests.length;
      if (vm.filteredcontests.length === 0) {
        vm.noContest = true;
      }
    }

    vm.searchTextChanged = function () {
      filtercontests(vm.searchText);
    };

    function getcontests() {
      contestsService.getcontests(vm.currentPage - 1, vm.pageSize)
        .then(function(data) {
          vm.totalRecords = data.totalRecords;
          vm.contests = data.results;
          filtercontests(''); //Trigger initial filter
        }, function(error) {
          $window.alert('Sorry, an error occurred: ' + error.data.message);
        });
    }

    vm.pageChanged = function(page) {
      vm.currentPage = page;
      getcontests();
    };

    function init() {
      if (!authService.user.isAuthenticated) {
        $location.path(authService.loginPath + '/lobby');
        return;
      }
      //createWatches();
      getcontests();
    }

    init();
  };

  lobbyController.$inject = injectParams;

  angular.module('fanSelectorApp').controller('LobbyController', lobbyController);

}());