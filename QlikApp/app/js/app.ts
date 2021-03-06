﻿/// <reference path="../../Scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../Scripts/typings/jquery/jquery.d.ts" />

'use strict';

// Declare app level module which depends on filters, and services
angular.module('qlik', [
	'qlik.services',
	'qlik.messagePane',
	'qlik.messagePoster'
])
	.directive('appVersion', ['version', function (version) {
		return function (scope, elm, attrs) {
			elm.text(version);
		};
	}]);