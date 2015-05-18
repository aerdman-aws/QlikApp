/// <reference path="../../Scripts/typings/angularjs/angular.d.ts" />
/// <reference path="./typings.d.ts" />

'use strict';

import IMessage = qlik.IMessage;
import IMessageDetail = qlik.IMessageDetail;
import IMessageService = qlik.IMessageService;

/* Controllers */
interface IMessagePaneController {
	loadDetails(id: number): void;
	deleteMessage(id: number): void;
}

interface IMessagePaneScope extends ng.IScope {
	controller: IMessagePaneController;

	messages: IMessage[];
}

interface IMessagePosterController {

}

interface IMessagePosterScope extends ng.IScope {
	controller: IMessagePosterController;

	message: IMessage;
}

class MessagePaneController implements IMessagePaneController {
	static $inject = ['$scope', 'messageService'];
	constructor(private $scope: IMessagePaneScope, private messageService: IMessageService) {
		$scope.controller = this;

		messageService.getAll().then((messages: IMessage[]) => {
			$scope.messages = messages;
		});
	}

	loadDetails(id: number): void {
		this.messageService.get(id).then((messageDetail: IMessageDetail) => {
			alert(messageDetail.Message.Body + ' is ' + (!messageDetail.IsPalindrome ? 'not ' : '') + 'a palindrome');
		});
	}

	deleteMessage(id: number): void {
		this.messageService.delete(id);
	}
}

class MessagePosterController implements IMessagePosterController {
	static $inject = ['$scope', 'messageService'];
	constructor(private $scope: IMessagePosterScope, private messageService: IMessageService) {
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

angular.module('qlik.controllers', [])
	.controller('MessagePaneController', MessagePaneController)
	.controller('MessagePosterController', MessagePosterController);