/// <reference path="../../../Scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../typings.d.ts" />

'use strict';

class MessagePosterDirective implements ng.IDirective {
	static $inject = [];
	constructor() { }

	scope = {};

	templateUrl = './app/js/messagePoster/messagePoster.html';
	controller = 'MessagePosterController';
};

class MessagePosterController implements qlik.IMessagePosterController {
	static $inject = ['$scope', 'messageService'];
	constructor(private $scope: qlik.IMessagePosterScope, private messageService: qlik.IMessageService) {
		$scope.controller = this;

		$scope.message = {
			Id: 0,
			Body: ''
		};
	}

	postMessage(): void {
		this.messageService.create(this.$scope.message);
		this.clearMessage(); //temp
	}

	clearMessage(): void {
		this.$scope.message.Body = '';
	}
}

angular.module('qlik.messagePoster', [])
	.directive('qlikMessagePoster', () => new MessagePosterDirective())
	.controller('MessagePosterController', MessagePosterController);