# QlikApp

QlikApp is an MIT licensed implementation of the Qlik Cloud Team Audition Project.

The project includes:

* **REST API** - A service that allows users to post messages, see all the messages that have been posted, delete specific messages, and retrieve a specific message on demand, and determine if it is a palindrome.
* **UI** - A simple interface to interact with the service. The UI shows the list of messages posted by the users, allows a user to post a new message, delete an existing message, and select a given message to see extra details.
* **EC2 Instance Deployer** - A simple application that can provision and deploy an EC2 Instance that contains everything needed to run the QlikApp.

## How to build and deply the app
The application is written with .Net technologies and is provided as a single solution.

* Use Visual Studio Community 2013 (or above) to build the solution: devenv "~\QlikApp.sln" /build Debug
* Configure the EC2 instance to deploy: ~\AwsConsole\bin\Debug\AwsConsole.exe.config
* Use the AwsConsole app to deploy the EC2 instance: ~\AwsConsole\bin\Debug\AwsConsole.exe
* Access the service at the address provided by the AwsConsole app once it completes
   
## Getting Started
Once the application has been deployed, it can be accessed via the EC2 instance's public address.

To access the Simple UI: 
* Navigate to <instance address>/qlikapp
 
To access the REST API: 
* Navigate to <instance address>/qlikapp/api/messages

## Solution structure
*The solution has been broken down into several projects*

QlikApp.Web
* The main project contains the REST API and the UI applications
* The REST API app is implemented using ASP.NET Web API 2 and is located in the WebApi folder
* The UI application is implemented using AngularJS and Typescript and is located in the app folder

QlikApp.Service
* The project that is used by QlikApp.Web to access and interact with messages in the system

QlikApp.Data
* The project that is used by QlikApp.Service to handle retrieving and persisting messages in the system

AwsConsole
* A command line application that is used to deploy the EC2 Instance that hosts the QlikApp
* Contains App.Config that defines all the cofniguration options for the EC2 Instance

AwsConsole.Services
* The project that is used by AwsConsole to read the deployment configuration and deploy the appropriate instance

## REST API Documentation
Access the REST API via: <host>/qlikapp/api/messages


* /api/messages

*List of messages*  
**Allowed Methods:** [ GET, POST ]  
**Arguments:** none  
**Model:** Message { id, body }  
**Description:** When used with GET, retrieves a list of all the messages in the system. When used with POST, pass a Message as data to create it in the system.

* /api/messages/{message_id}

*Message details*  
**Allowed Methods:** [ GET, DELETE ]  
**Arguments:** message_id as an integer  
**Model:** Message { id, body }  
**Description:** When used with GET, retrieves a specific message by Id, and determines if it is a palindrome. When used with DELETE, deletes the specified Message from the system.