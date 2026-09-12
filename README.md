# APARTMENT_API

A .NET 10 Web API for apartment building administration. The API provides JWT authentication and management endpoints for users, roles, permissions, buildings, floors, and guests.

## Features

- JWT bearer authentication
- User, role, permission, and role-permission management
- Building, floor, and guest management
- Oracle persistence with Entity Framework Core
- NSwag OpenAPI and Swagger UI documentation
- Repository and service layers
- AutoMapper-based DTO mapping
- Centralized exception handling
- Static file support for uploaded content

## Technology

- .NET 10
- ASP.NET Core Web API
- Oracle Database
- Entity Framework Core 10
- Oracle.EntityFrameworkCore
- NSwag
- AutoMapper
- Newtonsoft.Json

## Prerequisites

- .NET 10 SDK or later
- Oracle Database 10g or later, or a compatible Oracle instance
- Git
- Optional: `dotnet-ef` for creating and applying migrations

## Getting Started

Clone the repository and enter the project directory:

```bash
git clone https://github.com/Meng-Explorer/api-apartment.git
cd api-apartment
```

Restore dependencies and build the project:

```bash
dotnet restore
dotnet build
```

## Configuration

The local `appsettings*.json` files are intentionally excluded from Git because they can contain database credentials and JWT signing keys. Create a local `appsettings.Development.json` or use environment variables.

The application requires these settings:

```text
ConnectionStrings__DefaultConnection=<oracle-connection-string>
JWT__ValidIssuer=https://localhost:5191
JWT__ValidAudience=https://localhost:5191
JWT__Secret=<long-random-signing-key>
```

PowerShell example:

```powershell
$env:ConnectionStrings__DefaultConnection = "<oracle-connection-string>"
$env:JWT__ValidIssuer = "https://localhost:5191"
$env:JWT__ValidAudience = "https://localhost:5191"
$env:JWT__Secret = "<long-random-signing-key>"
```

Never commit real passwords, API keys, or JWT secrets. Use environment variables, .NET user secrets, or a managed secret store for local and production deployments.

## Database Migrations

Install the Entity Framework Core CLI if it is not already available:

```bash
dotnet tool install --global dotnet-ef
```

Apply the existing migrations:

```bash
dotnet ef database update
```

Create a new migration when the data model changes:

```bash
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

## Running the API

Run the application with:

```bash
dotnet run
```

The development launch profile uses the URLs defined in `Properties/launchSettings.json`.

## API Documentation

When the application is running, open:

- Swagger UI: `https://localhost:5191/swagger`
- OpenAPI JSON: `https://localhost:5191/swagger/v1/swagger.json`

The exact port can vary by launch profile. Check the console output or `Properties/launchSettings.json` if these URLs are unavailable.

## Project Structure

```text
APARTMENT_API/
├── Configurations/       Database context and AutoMapper setup
├── Controllers/          API controllers
├── DTOs/                 Request and response DTOs
├── Exceptions/           Application exception types
├── Helpers/              API response and query helpers
├── Middlewares/          Exception handling middleware
├── Migrations/           Entity Framework Core migrations
├── Models/               Entity models
├── Repositories/         Data access implementations and interfaces
├── Services/             Business logic implementations and interfaces
├── Properties/           Launch settings
├── Program.cs            Application startup and middleware configuration
└── APARTMENT_API.csproj  Project file
```

Current controllers include buildings, floors, guests, users, roles, permissions, role permissions, and user roles.

## Authentication

Obtain a JWT through the user authentication endpoint, then send it with protected requests:

```http
Authorization: Bearer <jwt-token>
```

The OpenAPI documentation describes the available endpoints and authorization requirements.

## Contributing

1. Create a feature branch.
2. Make focused changes and add tests where applicable.
3. Run `dotnet build` before opening a pull request.
4. Open a pull request with a description of the change.

## License

No license file is currently included in this repository. Add a license before distributing the project under specific open-source terms.
