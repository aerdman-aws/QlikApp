/// <reference path="../../../Scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../typings.d.ts" />

'use strict';

class MessagePaneDirective implements ng.IDirective {
	static $inject = [];
	constructor() { }

	scope = {};

	templateUrl = './app/js/messagePane/messagePane.html';
	controller = 'MessagePaneController';
};

class MessagePaneController implements qlik.IMessagePaneController {
	static $inject = ['$scope', 'messageService'];
	constructor(private $scope: qlik.IMessagePaneScope, private messageService: qlik.IMessageService) {
		$scope.controller = this;

		this.loadMessages();

		$scope.$watch('controller.messageService.isDirty', (newValue: boolean, oldValue: boolean): void => {
			if (!oldValue && newValue) { //if message collection wasn't dirty, but now is dirty...
				this.loadMessages(); //... reload the messages
			}
		});
	}

	private loadMessages(): void {
		this.messageService.getAll().then((messages: qlik.IMessage[]) => {
			this.$scope.messages = messages;
		});
	}

	loadDetails(id: number): void {
		this.messageService.get(id).then((messageDetail: qlik.IMessageDetail) => {
			alert('"' + messageDetail.Message.Body + '" is ' + (!messageDetail.IsPalindrome ? 'not ' : '') + 'a palindrome');
		});
	}

	deleteMessage(id: number): void {
		this.messageService.delete(id);
	}
}

angular.module('qlik.messagePane', [])
	.directive('qlikMessagePane', () => new MessagePaneDirective())
	.controller('MessagePaneController', MessagePaneController);