/// <reference path="../../Scripts/typings/angularjs/angular.d.ts" />
/// <reference path="./typings.d.ts" />

'use strict';

import IMessage = qlik.IMessage;
import IMessageService = qlik.IMessageService;

/* Controllers */
interface IMessagePaneController {

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
	
}

class MessagePosterController implements IMessagePosterController {
	static $inject = ['$scope', 'messageService'];
	constructor(private $scope: IMessagePosterScope, private messageService: IMessageService) {
		$scope.controller = this;

		$scope.message = {
			Id: '',
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