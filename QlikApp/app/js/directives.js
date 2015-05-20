/// <reference path="../../Scripts/typings/angularjs/angular.d.ts" />
/// <reference path="./typings.d.ts" />
'use strict';
/* Directives */
var MessagePaneDirective = (function () {
    function MessagePaneDirective() {
        this.scope = {};
        this.templateUrl = './app/partials/messagePane.html';
        this.controller = 'MessagePaneController';
    }
    MessagePaneDirective.$inject = [];
    return MessagePaneDirective;
})();
;
var MessagePosterDirective = (function () {
    function MessagePosterDirective() {
        this.scope = {};
        this.templateUrl = './app/partials/messagePoster.html';
        this.controller = 'MessagePosterController';
    }
    MessagePosterDirective.$inject = [];
    return MessagePosterDirective;
})();
;
angular.module('qlik.directives', []).directive('qlikMessagePane', function () { return new MessagePaneDirective(); }).directive('qlikMessagePoster', function () { return new MessagePosterDirective(); }).directive('appVersion', ['version', function (version) {
    return function (scope, elm, attrs) {
        elm.text(version);
    };
}]);
//# sourceMappingURL=directives.js.map