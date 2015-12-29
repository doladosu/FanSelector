(function() {

  var injectParams = ['$rootScope', 'config'];

  var signalRSvc = function($rootScope, config) {

    var proxy = null;

    var initialize = function() {
      //Getting the connection object
      var connection = $.hubConnection(config.serviceBase + 'signalr');
      //Creating proxy
      this.proxy = connection.createHubProxy('contest');

      //Starting connection
      connection.start();

      //Publishing an event when server pushes a message
      this.proxy.on('enteredContest', function (message) {
        $rootScope.$emit("enteredContest", message);
      });
    };

    var sendRequest = function() {
      //Invoking method defined in hub
      this.proxy.invoke('greetAll');
    };

    return {
      initialize: initialize,
      sendRequest: sendRequest
    };
  }
  signalRSvc.$inject = injectParams;

  angular.module('fanSelectorApp').service('signalRSvc', signalRSvc);

})();