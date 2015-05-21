declare module qlik {
	//A message in the system
	interface IMessage {
		Id: number; //A unique identifier for the message
		Body: string; //The actual message text
	}

	//A message in the system and additional information about it
	interface IMessageDetail {
		Message: IMessage; //The message
		IsPalindrome: boolean; //Whether the message text is a palindrome
	}

	//An interface that can access the REST API exposed to access, created, delete messages in the system
	interface IMessageApiService {
		get(apiUrl: string): ng.IPromise<any>; //performs a GET operation on the given REST endpoint
		post(apiUrl: string, data: any): ng.IPromise<any>; //performs a POST operation on the given REST endpoint, passing the provided data
		delete(apiUrl: string): ng.IPromise<any>; //performs a DELETE operation on the given REST endpoint
	}

	//A service to access, create, or delete messages in the sytem
	interface IMessageService {
		isDirty: boolean; //Whether or not the last collection of messages retrieved is known to be up-to-date

		create(message: IMessage): ng.IPromise<qlik.IMessage>; //Creates a new message in the system
		get(id: number): ng.IPromise<qlik.IMessageDetail>; //Retrieves the message from the system that matches the given message id
		getAll(): ng.IPromise<IMessage[]>; //Retrieves all the messages in the system
		delete(id: number): ng.IPromise<qlik.IMessage>; //Deletes the message from the system that matches the given message id
	}

	//The controller used by the message pane directive
	interface IMessagePaneController {
		loadDetails(id: number): void; //Loads additional information about the message with the given id
		deleteMessage(id: number): void; //Deletes the message with the given id
	}

	//The scope of the message pane directive
	interface IMessagePaneScope extends ng.IScope {
		controller: IMessagePaneController; //A reference to the controller

		messages: qlik.IMessage[]; //The collection of all messages that have been loaded
	}

	//The controller used by the message poster directive
	interface IMessagePosterController {

	}

	//The scope of the message poster directive
	interface IMessagePosterScope extends ng.IScope {
		controller: IMessagePosterController; //A reference to the controller

		message: qlik.IMessage; //The message being created by the user
	}
}