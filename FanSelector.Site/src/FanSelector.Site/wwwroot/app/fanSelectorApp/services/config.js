(function () {

    var value = {
      clientId: 'FanSelectorWeb',
      serviceBase: 'http://localhost:7519/'
    };

    angular.module('fanSelectorApp').value('config', value);

}());