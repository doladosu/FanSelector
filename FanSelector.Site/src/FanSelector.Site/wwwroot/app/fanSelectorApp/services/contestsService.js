(function() {
  var injectParams = ['$http', '$q', 'config'];

  var contestsFactory = function ($http, $q, config) {
    var serviceBase = config.serviceBase + 'api/',
        factory = {};

    // is this still used???
    function orderTotal(order) {
      return order.quantity * order.price;
    };

    function ordersTotal(customer) {
      var total = 0;
      var orders = customer.orders;
      var count = orders.length;

      for (var i = 0; i < count; i++) {
        total += orders[i].orderTotal;
      }
      return total;
    };

    function extendcontests(contests) {
      var custsLen = contests.length;
      //Iterate through contests
      for (var i = 0; i < custsLen; i++) {
        var cust = contests[i];
        if (!cust.orders) continue;

        var ordersLen = cust.orders.length;
        for (var j = 0; j < ordersLen; j++) {
          var order = cust.orders[j];
          order.orderTotal = order.quantity * order.price;
        }
        cust.ordersTotal = ordersTotal(cust);
      }
    }

    function buildPagingUri(pageIndex, pageSize) {
      var uri = '?$top=' + pageSize + '&$skip=' + (pageIndex * pageSize);
      return uri;
    }

    function getPagedResource(baseResource, pageIndex, pageSize) {
      var resource = baseResource;
      resource += (arguments.length == 3) ? buildPagingUri(pageIndex, pageSize) : '';
      return $http.get(serviceBase + resource).then(function (response) {
        var custs = response.data;
        extendcontests(custs);
        return {
          totalRecords: parseInt(response.headers('X-InlineCount')),
          results: custs
        };
      });
    }

    factory.getcontests = function (pageIndex, pageSize) {
      return getPagedResource('contests', pageIndex, pageSize);
    };

    factory.getStates = function () {
      return $http.get(serviceBase + 'states').then(
          function (results) {
            return results.data;
          });
    };

    factory.checkUniqueValue = function (id, property, value) {
      if (!id) id = 0;
      return $http.get(serviceBase + 'checkUnique/' + id + '?property=' + property + '&value=' + escape(value)).then(
          function (results) {
            return results.data.status;
          });
    };

    factory.insertCustomer = function (customer) {
      return $http.post(serviceBase + 'postCustomer', customer).then(function (results) {
        customer.id = results.data.id;
        return results.data;
      });
    };

    factory.newCustomer = function () {
      return $q.when({ id: 0 });
    };

    factory.updateCustomer = function (customer) {
      return $http.put(serviceBase + 'putCustomer/' + customer.id, customer).then(function (status) {
        return status.data;
      });
    };

    factory.deleteCustomer = function (id) {
      return $http.delete(serviceBase + 'deleteCustomer/' + id).then(function (status) {
        return status.data;
      });
    };

    factory.getCustomer = function (id) {
      //then does not unwrap data so must go through .data property
      //success unwraps data automatically (no need to call .data property)
      return $http.get(serviceBase + 'customerById/' + id).then(function (results) {
        extendcontests([results.data]);
        return results.data;
      });
    };

    return factory;
  };

  contestsFactory.$inject = injectParams;

  angular.module('fanSelectorApp').factory('contestsService', contestsFactory);

}());