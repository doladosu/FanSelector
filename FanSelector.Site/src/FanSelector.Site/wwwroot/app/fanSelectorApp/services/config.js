(function () {

  var value = {
    clientId: 'FanSelectorWeb',
    serviceBase: 'http://fanselectorapi.azurewebsites.net/api/' //'http://localhost:7519/api/'
};

    angular.module('fanSelectorApp').value('config', value);

}());