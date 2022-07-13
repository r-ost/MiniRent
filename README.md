 # MiniRent 
 [![Build Status](https://dev.azure.com/MiniRent/MiniRent/_apis/build/status/r-ost.MiniRent?branchName=develop)](https://dev.azure.com/MiniRent/MiniRent/_build/latest?definitionId=1&branchName=develop)

App: https://minirent.online/

Api documentation (swagger): https://minirent.online/api

<br/>

MiniRent is a web application made for the university project. It allows you to rent cars from many imaginary car rental companies.

 Each company must provide REST API, that follow specific contract (https://mini.rentcar.api.snet.com.pl/swagger). MiniRent backend then calls these APIs to fetch available vehicles and prices. MiniRent allows to rent cars and return them. And lastly, there is a page to browse historic rentals.

 Application is written based on Jason Taylor's [CleanArchitecture project template](https://github.com/jasontaylordev/CleanArchitecture). It uses CQRS pattern, elements of DDD (e.g. ValueObjects) and separation to Domain, Application, Infrastructure, WebUIa.

 Application is secured with OAuth 2.0 (authorization code flow with PKCE) using Azure AD (you must have Microsoft account to sign up to the app). One of the external APIs is also secured with OAuth 2.0 (https://mini.rentcar.api.snet.com.pl/swagger).



## Technologies

* [ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
* [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core/)
* [React](https://reactjs.org/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [Refit](https://github.com/reactiveui/refit)
* [FluentValidation](https://fluentvalidation.net/)
* [NSwag](https://github.com/RicoSuter/NSwag)
* [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/)
* [Docker](https://www.docker.com/)

## Run application locally

1. Install the latest [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
2. Install the latest [Node.js LTS](https://nodejs.org/en/)
3. Navigate to `src/WebUI/ClientApp` and run `npm install`
4. Navigate to `src/WebUI/ClientApp` and run `npm start` to launch the front end (React)
5. Navigate to `src/WebUI` and run `dotnet run` to launch the back end (ASP.NET Core Web API)
6. Go to http://localhost:5000/

## Run in Docker

1. Navigate to repository main directory.
2. Run:
```
$ docker-compose -f .\docker-compose.yml up --build -d
```
3. Go to http://localhost:5000/

## CI/CD

Azure DevOps - https://dev.azure.com/MiniRent/MiniRent.
* build an release pipelines
* self hosted build agent on Azure VM
* self hosted deployment agent on Azure VM

## Hosting

Hosted on Azure Virtual Machine using Docker, Nginx, Let's encrypt.

## Connected APIs
* https://mini.rentcar.api.snet.com.pl/swagger
* self written api with the same contract as above; hosted on VM using Docker but is only accessible from MiniRent backend

## Database

### Database Configuration

If you would like to use SQL Server, you will need to update **WebUI/appsettings.json** as follows:

```json
  "UseInMemoryDatabase": false,
```

Verify that the **DefaultConnection** connection string within **appsettings.json** points to a valid SQL Server instance. 

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.

### Database Migrations

To use `dotnet-ef` for your migrations please add the following flags to your command (values assume you are executing from repository root)

* `--project src/Infrastructure` (optional if in this folder)
* `--startup-project src/WebUI`
* `--output-dir Persistence/Migrations`

For example, to add a new migration from the root folder:

 `dotnet ef migrations add "SampleMigration" --project src\Infrastructure --startup-project src\WebUI --output-dir Persistence\Migrations`
