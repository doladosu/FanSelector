﻿(function() {

    var injectParams = [
        '$scope', '$location', '$routeParams',
        'config', 'modalService', 'signalRSvc', 'contestsService'
    ];

    var enterContestController = function($scope, $location, $routeParams,
        config, modalService, signalRSvc, contestsService) {

        var vm = this,
            id = ($routeParams.id) ? parseInt($routeParams.id) : 0,
            timer,
            onRouteChangeOff;

        vm.customer = {};
        vm.states = [];
        vm.title = 'Enter Contests Id: ' + id;
        vm.updateStatus = false;
        vm.errorMessage = '';

        vm.selectedFormation = '442';
        var formation1 = { id: "442", name: "4-4-2" };
        var formation2 = { id: "433", name: "4-3-3" };
        var formation3 = { id: "352", name: "3-5-2" };
        vm.formations = [];
        vm.formations.push(formation1);
        vm.formations.push(formation2);
        vm.formations.push(formation3);
        signalRSvc.initialize();

        $scope.$parent.$on("enteredContest", function(e, message) {
            $scope.$apply(function() {
                console.log(message);
            });
        });

        function getContest() {
            contestsService.getContest(vm.id)
            .then(function (data) {
                vm.contest = data.results;
            }, function (error) {
                $window.alert('Sorry, an error occurred: ' + error.data.message);
            });
        }

        function init() {
            getContest();
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

            modalService.showModal({}, modalOptions).then(function(result) {
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