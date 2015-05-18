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
angular.module('qlik.controllers', []).controller('MessagePaneController', MessagePaneController);
//# sourceMappingURL=controllers.js.map