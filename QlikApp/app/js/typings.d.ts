declare module qlik {
	interface IMessage {
		Id: number;
		Body: string;
	}

	interface IMessageApiService {
		get(apiUrl: string): ng.IPromise<any>;
		post(apiUrl: string, data: any): ng.IPromise<any>;
		delete(apiUrl: string): ng.IPromise<any>;
	}

	interface IMessageService {
		getAll(): ng.IPromise<IMessage[]>;
		create(message: IMessage): ng.IPromise<qlik.IMessage>;
		delete(id: number): ng.IPromise<qlik.IMessage>;
	}
}