declare module qlik {
	interface IMessage {
		Id: number;
		Body: string;
	}

	interface IMessageDetail {
		Message: IMessage;
		IsPalindrome: boolean;
	}

	interface IMessageApiService {
		get(apiUrl: string): ng.IPromise<any>;
		post(apiUrl: string, data: any): ng.IPromise<any>;
		delete(apiUrl: string): ng.IPromise<any>;
	}

	interface IMessageService {
		isDirty: boolean;

		getAll(): ng.IPromise<IMessage[]>;
		get(id: number): ng.IPromise<qlik.IMessageDetail>;
		create(message: IMessage): ng.IPromise<qlik.IMessage>;
		delete(id: number): ng.IPromise<qlik.IMessage>;
	}
}