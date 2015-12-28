(function () {

    var injectParams = ['$scope', '$location', '$routeParams',
                        '$timeout', 'config', 'dataService', 'modalService'];

    var enterContestController = function ($scope, $location, $routeParams,
                                           $timeout, config, dataService, modalService) {

      var vm = this,
          id = ($routeParams.id) ? parseInt($routeParams.id) : 0,
          timer,
          onRouteChangeOff,
          hub = $.connection.contests;

        vm.customer = {};
        vm.states = [];
        vm.title = 'Enter Contests Id: ' + id;
        vm.updateStatus = false;
        vm.errorMessage = '';


        hub.enteredContest = function (item) {

        };

        function init() {

            //Make sure they're warned if they made a change but didn't save it
            //Call to $on returns a "deregistration" function that can be called to
            //remove the listener (see routeChange() for an example of using it)
            onRouteChangeOff = $scope.$on('$locationChangeStart', routeChange);
        }

        init();

        function routeChange(event, newUrl, oldUrl) {
            //Navigate to newUrl if the form isn't dirty
            if (!vm.editForm || !vm.editForm.$dirty) return;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Ignore Changes',
                headerText: 'Unsaved Changes',
                bodyText: 'You have unsaved changes. Leave the page?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    onRouteChangeOff(); //Stop listening for location changes
                    $location.path($location.url(newUrl).hash()); //Go to page they're interested in
                }
            });

            //prevent navigation by default since we'll handle it
            //once the user selects a dialog option
            event.preventDefault();
            return;
        }
    };

    enterContestController.$inject = injectParams;

    angular.module('fanSelectorApp').controller('EnterContestController', enterContestController);

}());