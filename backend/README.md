# Book Manager System
A simple book management system built with .NET, Entity Framework Core, and RESTful principles, designed to manage books, authors, categories, and related entities while demonstrating clean architecture principles and efficient development workflows. This project is a **work in progress** and as such, changes and unfinished features are expected.

## Features
- API-driven: Exposes RESTful endpoints for managing books, authors, categories, and other entities.
- Entity Framework Core: Leverages EF Core for database interactions with full support for migrations.
- Devcontainer Support: Ready-to-use development environment with Docker and VS Code's [Dev Containers](https://code.visualstudio.com/docs/devcontainers/containers).
- xUnit Tests: Ensures code quality with unit testing.

## Getting Started

### Requirements
It's [required](https://code.visualstudio.com/docs/devcontainers/containers#_system-requirements) that either:
- Docker is installed locally.
- Docker installed on a remote environment

### Configuring the container
Any changes in the development environment can be made on the files (it will require the container to be rebuild):
- [.devcontainer/devcontainer.json](.devcontainer/devcontainer.json) for metada and settings of the development container
- [.devcontainer/Dockerfile](.devcontainer/Dockerfile) for the main Docker image in use. Currently using [mcr.microsoft.com/devcontainers/dotnet](https://hub.docker.com/r/microsoft/devcontainers-dotnet) with dotnet 9.0
- [.devcontainer/docker-compose.yml](.devcontainer/docker-compose.yml) for the db orchestration. Currently using [mcr.microsoft.com/mssql/server](https://hub.docker.com/r/microsoft/mssql-server) with the 2019 version.


### Running Dev Containers
Follow these steps to get started:
1. Open the project in VS Code.
2. Install the [Dev Containers](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers) extension.
3. Reopen the project in the Dev Container:
4. Press Ctrl+Shift+P -> Remote-Containers: Reopen in Container.
5. Wait for the container to build and start. All dependencies will be installed automatically.

### Setup
1. Run the following commands to set up the database:
`dotnet ef database update`
2. Start the application:
`dotnet run`
3. The API will be available at `http://localhost:5082`.

## Project Structure
The project is organized into the following layers:

### Controllers
Handles incoming API requests and directs them to the appropriate services. Controllers are responsible for HTTP status codes and responses.

### Services
Contains all business logic and acts as an intermediary between controllers and the data layer. Also includes mappings between DTOs and entities.

### Models
Defines the logical structure of the data, including entities for EF Core and DTOs for API requests/responses.

## Scripts
The create_all.sh script automates the creation of new entities by generating:

- Models
- Services
- Controllers
It also updates Program.cs to include the new services automatically.

For running it, use the following command:
`./create_all.sh ENTITY_NAME ENTITY_FOLDER CRUD`

Where:
- `ENTITY_NAME` is the entity model name.
- `ENTITY_FOLDER` is where the entity model and service will be store within `src/Entities` and `src/Services`.
- `CRUD` if the default crud service will be used in the service/controller. Still a **work in progress** but works for simple API call without any DTO mappings or complex relationships.

## Data Model Diagram
Below is the data model diagram representing the relationships between books, authors, categories, and other entities:
![data model diagram](/docs/diagram.png)

> [!NOTE]
> The following section has not started. Currently just a place holder text for future reminder.
## Unit Testing
Unit testing is implemented using xUnit. The tests cover critical business logic and ensure the system functions as expected.

### Running Tests:
Execute the following command to run the tests:
`dotnet test`

## TODO
- [X] Refactor models navigation properties and ids
- [X] Add DTOs and refactor models/controllers
- [X] Use Include/ThenInclude instead of multiple queries on BooksCategories and others
- [X] Update all references of Set<T> for the right context sets
- [ ] Finish order controller/service
- [ ] Add tests for controllers and services
- [X] Add pagination
- [ ] Add testing for pagination
- [ ] Add authentication
- [ ] Add testing for auth
- [ ] Add role based acess control
- [ ] Admin role should be able to renable deleted entities
- [ ] Choose either Angular or React for the frontend portion
- [ ] Maybe refactor code for repository pattern instead context direct acess