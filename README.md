# todo-api
A todo restful api application developed with .NET Core 7.
This repository contains by 3 projects that work together to provide todo services.
1. Todo.Api - Provides the entry points into application services and define API interfaces.
2. Todo.Application - Encapsluate all the business logic and core operation and provides interfaces to other project to consume the services
3. Todo.Test - Unit tests for all business logic, core operation and authentication

Application Layers:
1. Authentication - Login will generate a Json Web Token and return client, every request to any todo api have to include the JWT at request header.  
2. Dependency Injection (DI) - When the application is started or recevied request, this layer will construct object like Service and Data Repository, and will consumed by the components including API controller and services.  
3. API Controllers - Entry point for Https request and defining the route patterns, request will be handled by calling corresponding services and complete the operation. Finally return a HttpResponse
4. Application Services - Core component that encapsulate all core business logic and implement the operation detail.
5. Database - In-memory database is used in this project for simplicity. Can easily switch to use other ORM framework due to well-defined interfaces by Repository pattern and DI to de-couple the application from database layer.

Other Library used:
1. FluentResult - Improve the flow control of application by replacing the usage of Exception class to Result class. Following the principle that "Only throw an exception to state an unexpected situation in your software (ref: https://enterprisecraftsmanship.com/posts/exceptions-for-flow-control/)"
2. FluentAssert - More declaritive assertion method and easy-to-use methods to do assertion on complex Data Structure
3. AutoFixture - Easy-to-use library to auto generate fake objects for testing purpose
4. Mapster - Model mapping library to map object into another class.
5. Moq - Mock library to easily mock object, really useful in writing unit test to isolate other components from single method to ensure the test only involve single component.

Future possible improvement:
1. Integration test
2. Request validation using nuget package FluentValidation to validate request body to avoid invalid request. 
3. Add more detail into the OpenApi documentation
