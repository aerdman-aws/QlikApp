/// <reference path="../../Scripts/typings/angularjs/angular.d.ts" />
/// <reference path="./typings.d.ts" />

'use strict';

/* Services */

class MessageApiService implements qlik.IMessageApiService {
	static $inject = ['$http', '$q', '$log'];
	constructor(private $http: ng.IHttpService, private $q: ng.IQService, private $log: ng.ILogService) {
	}

	get(apiUrl: string): ng.IPromise<any> {
		var deferred = this.$q.defer<any>();

		var url: string = '../api/messages/' + apiUrl;

		this.$log.debug('Request: ', url);

		this.$http.get(url)
			.success((data: any, status: number, headers: ng.IHttpHeadersGetter, config: ng.IRequestConfig): void => {
				this.$log.debug('Response: ', data);
				deferred.resolve(data);
			})
			.error((data: any, status: number, headers: ng.IHttpHeadersGetter, config: ng.IRequestConfig): void => {
				this.$log.error('Error: ', data);
				deferred.reject(data);
			});

		return deferred.promise;
	}

	post(apiUrl: string, data: any): ng.IPromise<any> {
		var deferred = this.$q.defer<any>();

		var url: string = '../api/messages/' + apiUrl;

		this.$log.debug('Request: ', url);

		this.$http.post(url, data)
			.success((data: any, status: number, headers: ng.IHttpHeadersGetter, config: ng.IRequestConfig): void => {
				this.$log.debug('Response: ', data);
				deferred.resolve(data);
			})
			.error((data: any, status: number, headers: ng.IHttpHeadersGetter, config: ng.IRequestConfig): void => {
				this.$log.error('Error: ', data);
				deferred.reject(data);
			});

		return deferred.promise;
	}

	delete(apiUrl: string): ng.IPromise<any> {
		var deferred = this.$q.defer<any>();

		var url: string = '../api/messages/' + apiUrl;

		this.$log.debug('Request: ', url);

		this.$http.delete(url)
			.success((data: any, status: number, headers: ng.IHttpHeadersGetter, config: ng.IRequestConfig): void => {
				this.$log.debug('Response: ', data);
				deferred.resolve(data);
			})
			.error((data: any, status: number, headers: ng.IHttpHeadersGetter, config: ng.IRequestConfig): void => {
				this.$log.error('Error: ', data);
				deferred.reject(data);
			});

		return deferred.promise;
	}
}

class MessageService implements qlik.IMessageService {
	static $inject = ['$q', '$log', 'messageApiService'];
	constructor(private $q: ng.IQService, private $log: ng.ILogService, private api: qlik.IMessageApiService) {
	}

	getAll(): ng.IPromise<qlik.IMessage[]> {
		var deferred = this.$q.defer<qlik.IMessage[]>();

		var url = '';
		this.api.get(url)
			.then((result: any) => {
				var messages: IMessage[] = <qlik.IMessage[]>result;
				deferred.resolve(messages);
			})
			.catch((reason: any) => {
				deferred.reject(reason);
			});

		return deferred.promise;
	}

	create(message: IMessage): ng.IPromise<qlik.IMessage> {
		var deferred = this.$q.defer<qlik.IMessage>();

		var url = '';
		this.api.post(url, { Id: message.Id, Body: message.Body })
			.then((result: any) => {
				var message: IMessage = <qlik.IMessage>result;
				deferred.resolve(message);
			})
			.catch((reason: any) => {
				deferred.reject(reason);
			});

		return deferred.promise;
	}

	delete(id: number): ng.IPromise<qlik.IMessage> {
		var deferred = this.$q.defer<qlik.IMessage>();

		var url = id.toString();
		this.api.delete(url)
			.then((result: any) => {
				var message: IMessage = <qlik.IMessage>result;
				deferred.resolve(message);
			})
			.catch((reason: any) => {
				deferred.reject(reason);
			});

		deferred.resolve(null);

		return deferred.promise;
	}
}

angular.module('qlik.services', [])
	.service('messageApiService', MessageApiService)
	.service('messageService', MessageService)
	.value('version', '0.1');
