/// <reference path="../../Scripts/typings/angularjs/angular.d.ts" />
/// <reference path="./typings.d.ts" />
'use strict';
/* Directives */
var MessagePaneDirective = (function () {
    function MessagePaneDirective() {
        this.scope = {};
        this.templateUrl = './partials/messagePane.html';
        this.controller = 'MessagePaneController';
    }
    MessagePaneDirective.$inject = [];
    return MessagePaneDirective;
})();
;
angular.module('qlik.directives', []).directive('qlikMessagePane', function () { return new MessagePaneDirective(); }).directive('appVersion', ['version', function (version) {
    return function (scope, elm, attrs) {
        elm.text(version);
    };
}]);
//# sourceMappingURL=directives.js.map