(function () {

    var injectParams = ['$location', '$filter', '$window',
                        '$timeout', 'authService', 'contestsService', 'modalService'];

    var lobbyController = function ($location, $filter, $window,
        $timeout, authService, contestsService, modalService) {

        var vm = this;

        vm.contests = [];
        vm.filteredcontests = [];
        vm.filteredCount = 0;
        vm.orderby = 'contestDate';
        vm.reverse = false;
        vm.searchText = null;
        vm.cardAnimationClass = '.card-animation';

        //paging
        vm.totalRecords = 0;
        vm.pageSize = 10;
        vm.currentPage = 1;

        vm.deletecontest = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var cust = getcontestById(id);
            var custName = cust.firstName + ' ' + cust.lastName;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete contest',
                headerText: 'Delete ' + custName + '?',
                bodyText: 'Are you sure you want to delete this contest?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    contestsService.deletecontest(id).then(function () {
                        for (var i = 0; i < vm.contests.length; i++) {
                            if (vm.contests[i].id === id) {
                                vm.contests.splice(i, 1);
                                break;
                            }
                        }
                        filtercontests(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting contest: ' + error.message);
                    });
                }
            });
        };

        vm.DisplayModeEnum = {
            Card: 0,
            List: 1
        };

        vm.changeDisplayMode = function (displayMode) {
            switch (displayMode) {
                case vm.DisplayModeEnum.Card:
                    vm.listDisplayModeEnabled = false;
                    break;
                case vm.DisplayModeEnum.List:
                    vm.listDisplayModeEnabled = true;
                    break;
            }
        };

        vm.navigate = function (url) {
            $location.path(url);
        };

        vm.setOrder = function (orderby) {
            if (orderby === vm.orderby) {
                vm.reverse = !vm.reverse;
            }
            vm.orderby = orderby;
        };

        vm.searchTextChanged = function () {
            filtercontests(vm.searchText);
        };

        function filtercontests(filterText) {
          vm.filteredcontests = $filter("nameCityStateFilter")(vm.contests, filterText);
          vm.filteredCount = vm.filteredcontests.length;
        }

        function getcontests() {
          contestsService.getcontests(vm.currentPage - 1, vm.pageSize)
          .then(function (data) {
            vm.totalRecords = data.totalRecords;
            vm.contests = data.results;
            filtercontests(''); //Trigger initial filter

            $timeout(function () {
              vm.cardAnimationClass = ''; //Turn off animation since it won't keep up with filtering
            }, 1000);

          }, function (error) {
            $window.alert('Sorry, an error occurred: ' + error.data.message);
          });
        }

        vm.pageChanged = function (page) {
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

      //function createWatches() {
        //    //Watch searchText value and pass it and the contests to nameCityStateFilter
        //    //Doing this instead of adding the filter to ng-repeat allows it to only be run once (rather than twice)
        //    //while also accessing the filtered count via vm.filteredCount above

        //    //Better to handle this using ng-change on <input>. See searchTextChanged() function.
        //    vm.$watch("searchText", function (filterText) {
        //        filtercontests(filterText);
        //    });
        //}

        function getcontestById(id) {
            for (var i = 0; i < vm.contests.length; i++) {
                var cust = vm.contests[i];
                if (cust.id === id) {
                    return cust;
                }
            }
            return null;
        }

        init();
    };

    lobbyController.$inject = injectParams;

    angular.module('fanSelectorApp').controller('LobbyController', lobbyController);

}());
