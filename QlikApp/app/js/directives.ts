/// <reference path="../../Scripts/typings/angularjs/angular.d.ts" />
/// <reference path="./typings.d.ts" />

'use strict';

/* Directives */

class MessagePaneDirective implements ng.IDirective {
	static $inject = [];
	constructor() { }

	scope = { };

	templateUrl = './partials/messagePane.html';
	controller = 'MessagePaneController';
};

angular.module('qlik.directives', [])
	.directive('qlikMessagePane', () => new MessagePaneDirective())
	.directive('appVersion', ['version', function (version) {
		return function (scope, elm, attrs) {
			elm.text(version);
		};
	}]);
