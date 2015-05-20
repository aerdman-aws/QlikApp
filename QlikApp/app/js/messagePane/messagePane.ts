/// <reference path="../../../Scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../typings.d.ts" />

'use strict';

/* Directives */

interface IMessagePaneController {
	loadDetails(id: number): void;
	deleteMessage(id: number): void;
}

interface IMessagePaneScope extends ng.IScope {
	controller: IMessagePaneController;

	messages: qlik.IMessage[];
}

class MessagePaneDirective implements ng.IDirective {
	static $inject = [];
	constructor() { }

	scope = {};

	templateUrl = './app/js/messagePane/messagePane.html';
	controller = 'MessagePaneController';
};

class MessagePaneController implements IMessagePaneController {
	static $inject = ['$scope', 'messageService'];
	constructor(private $scope: IMessagePaneScope, private messageService: qlik.IMessageService) {
		$scope.controller = this;

		messageService.getAll().then((messages: qlik.IMessage[]) => {
			$scope.messages = messages;
		});
	}

	loadDetails(id: number): void {
		this.messageService.get(id).then((messageDetail: qlik.IMessageDetail) => {
			alert(messageDetail.Message.Body + ' is ' + (!messageDetail.IsPalindrome ? 'not ' : '') + 'a palindrome');
		});
	}

	deleteMessage(id: number): void {
		this.messageService.delete(id);
	}
}

angular.module('qlik.messagePane', [])
	.directive('qlikMessagePane', () => new MessagePaneDirective())
	.controller('MessagePaneController', MessagePaneController);