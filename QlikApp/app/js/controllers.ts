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

class MessagePaneController implements IMessagePaneController {
	static $inject = ['$scope', 'messageService'];
	constructor(private $scope: IMessagePaneScope, private messageService: IMessageService) {
		$scope.controller = this;

		messageService.getAll().then((messages: IMessage[]) => {
			$scope.messages = messages;
		});
	}
	
}


angular.module('qlik.controllers', [])
	.controller('MessagePaneController', MessagePaneController);