# ChatAggregator

This project is a .NET 7 application that provides a chat room interface. The application allows users to view chat history at varying levels of time-based aggregation. This project uses a json file to load the chat events. 

## Features

- View chat history in descending chronological order.
- The ability to see chat events aggregated by minute, hour, or day.
- Events types supported: "enter-the-room", "leave-the-room", "comment", and "high-five-another-user".
- Built using Clean Architecture principles.
- Unit testing and architecture testing

## Project Structure

- `ChatAggregator.Domain`: This project contains the business entities related to the chat functionality.
- `ChatAggregator.Core`: This project contains the business logic and use cases of the system. It depends on the `Domain` project.
- `ChatAggregator.Infrastructure`: This is an empty project to follow the clean code architecture practice. As there was no infrastructure layer needed for this project yet, hence this is not implemented. Infrastructure project can later be extended to contain classes for data access, file handling, and other low-level operations. It will depend on the `Core` and `Domain` projects.
- `ChatAggregator.Web`: This is the Web API project. It depends on the `Core` and `Infrastructure` projects. It can be extended to have a UI which will use the same endpoints/ gateways.

## How to Run

1. Clone the repository.
2. Open the solution file (.sln) in Visual Studio or Rider.
3. Build the solution (Build -> Build Solution).
4. After running the `src\ChatAggregator.Web` project, use the endpoint `GET /ChatRoom?granularity=xxx` where `xxx` can be "Minute", "Hourly", or "Daily" to fetch the chat history aggregated at the specified level.

## Architecture

This project uses Clean Architecture that emphasizes separation of concerns within the system. It enables the software to have separate software elements, independence of layers, and encapsulation. 

The main goals of Clean Architecture include:

- **Independence of Frameworks**: The architecture does not depend on specific libraries or software frameworks. This allows for flexibility in using such tools and enables ease in upgrading or replacing them.
- **Testability**: The business rules can be tested independently of the UI, Database, Web Server, or any other external element.
- **Independence of UI**: The UI can change without affecting the rest of the system. A Web UI could be replaced with a console UI, for instance, and the business rules would be unaffected.
- **Independence of Database**: The system doesn't need to be bound to a particular database. The business rules are not bound to the database details.

In this project, `ChatAggregator.Domain` represents the Domain Layer, `ChatAggregator.Core` represents the Use Case/Application Layer, `ChatAggregator.Infrastructure` is the Infrastructure Layer (which includes gateways and adapters like databases or web services), and `ChatAggregator.Web` represents the UI Layer or Web layer. This structure enables a loose coupling between layers, promoting maintainability and scalability of the application. 

A simple architecture diagram of the current implementation is given below. 

![alt text](https://github.com/anam294/chat-history-aggregator/blob/8581ad1111f38a07b582671d724bde19c56318ab/Architecture.png)

To ensure architecture rules are implemented correctly, unit tests include architectural tests to check for any inconsistencies. 