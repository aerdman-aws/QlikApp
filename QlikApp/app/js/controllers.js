/// <reference path="../../Scripts/typings/angularjs/angular.d.ts" />
/// <reference path="./typings.d.ts" />
'use strict';
var MessagePaneController = (function () {
    function MessagePaneController($scope, messageService) {
        this.$scope = $scope;
        this.messageService = messageService;
        $scope.controller = this;
        messageService.getAll().then(function (messages) {
            $scope.messages = messages;
        });
    }
    MessagePaneController.$inject = ['$scope', 'messageService'];
    return MessagePaneController;
})();
var MessagePosterController = (function () {
    function MessagePosterController($scope, messageService) {
        this.$scope = $scope;
        this.messageService = messageService;
        $scope.controller = this;
        $scope.message = {
            Id: '',
            Body: ''
        };
    }
    MessagePosterController.prototype.postMessage = function () {
        this.messageService.create(this.$scope.message);
        this.clearMessage(); //temp
    };
    MessagePosterController.prototype.clearMessage = function () {
        this.$scope.message.Body = '';
    };
    MessagePosterController.$inject = ['$scope', 'messageService'];
    return MessagePosterController;
})();
angular.module('qlik.controllers', []).controller('MessagePaneController', MessagePaneController).controller('MessagePosterController', MessagePosterController);
//# sourceMappingURL=controllers.js.map