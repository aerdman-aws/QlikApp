/// <reference path="../../Scripts/typings/angularjs/angular.d.ts" />
/// <reference path="./typings.d.ts" />

'use strict';

/* Directives */

class MessagePaneDirective implements ng.IDirective {
	static $inject = [];
	constructor() {}

	scope = {};

	templateUrl = './partials/messagePane.html';
	controller = 'MessagePaneController';
};

class MessagePosterDirective implements ng.IDirective {
	static $inject = [];
	constructor() {}

	scope = {};

	templateUrl = './partials/messagePoster.html';
	controller = 'MessagePosterController';
};

angular.module('qlik.directives', [])
	.directive('qlikMessagePane', () => new MessagePaneDirective())
	.directive('qlikMessagePoster', () => new MessagePosterDirective())
	.directive('appVersion', ['version', function (version) {
		return function (scope, elm, attrs) {
			elm.text(version);
		};
	}]);
