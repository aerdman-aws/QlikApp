/// <reference path="../../Scripts/typings/angularjs/angular.d.ts" />
/// <reference path="./typings.d.ts" />
'use strict';
/* Services */
var MessageApiService = (function () {
    function MessageApiService($http, $q, $log) {
        this.$http = $http;
        this.$q = $q;
        this.$log = $log;
    }
    MessageApiService.prototype.get = function (apiUrl) {
        var _this = this;
        var deferred = this.$q.defer();
        var url = '../api/messages/' + apiUrl;
        this.$log.debug('Request: ', url);
        this.$http.get(url).success(function (data, status, headers, config) {
            _this.$log.debug('Response: ', data);
            deferred.resolve(data);
        }).error(function (data, status, headers, config) {
            _this.$log.error('Error: ', data);
            deferred.reject(data);
        });
        return deferred.promise;
    };
    MessageApiService.prototype.post = function (apiUrl, data) {
        var _this = this;
        var deferred = this.$q.defer();
        var url = '../api/messages/' + apiUrl;
        this.$log.debug('Request: ', url);
        this.$http.post(url, data).success(function (data, status, headers, config) {
            _this.$log.debug('Response: ', data);
            deferred.resolve(data);
        }).error(function (data, status, headers, config) {
            _this.$log.error('Error: ', data);
            deferred.reject(data);
        });
        return deferred.promise;
    };
    MessageApiService.prototype.delete = function (apiUrl) {
        var _this = this;
        var deferred = this.$q.defer();
        var url = '../api/messages/' + apiUrl;
        this.$log.debug('Request: ', url);
        this.$http.delete(url).success(function (data, status, headers, config) {
            _this.$log.debug('Response: ', data);
            deferred.resolve(data);
        }).error(function (data, status, headers, config) {
            _this.$log.error('Error: ', data);
            deferred.reject(data);
        });
        return deferred.promise;
    };
    MessageApiService.$inject = ['$http', '$q', '$log'];
    return MessageApiService;
})();
var MessageService = (function () {
    function MessageService($q, $log, api) {
        this.$q = $q;
        this.$log = $log;
        this.api = api;
    }
    MessageService.prototype.getAll = function () {
        var deferred = this.$q.defer();
        var url = '';
        this.api.get(url).then(function (result) {
            var messages = result;
            deferred.resolve(messages);
        }).catch(function (reason) {
            deferred.reject(reason);
        });
        return deferred.promise;
    };
    MessageService.prototype.get = function (id) {
        var deferred = this.$q.defer();
        var url = id.toString();
        this.api.get(url).then(function (result) {
            var message = result;
            deferred.resolve(message);
        }).catch(function (reason) {
            deferred.reject(reason);
        });
        return deferred.promise;
    };
    MessageService.prototype.create = function (message) {
        var deferred = this.$q.defer();
        var url = '';
        this.api.post(url, { Id: message.Id, Body: message.Body }).then(function (result) {
            var message = result;
            deferred.resolve(message);
        }).catch(function (reason) {
            deferred.reject(reason);
        });
        return deferred.promise;
    };
    MessageService.prototype.delete = function (id) {
        var deferred = this.$q.defer();
        var url = id.toString();
        this.api.delete(url).then(function (result) {
            var message = result;
            deferred.resolve(message);
        }).catch(function (reason) {
            deferred.reject(reason);
        });
        deferred.resolve(null);
        return deferred.promise;
    };
    MessageService.$inject = ['$q', '$log', 'messageApiService'];
    return MessageService;
})();
angular.module('qlik.services', []).service('messageApiService', MessageApiService).service('messageService', MessageService).value('version', '0.1');
//# sourceMappingURL=services.js.map