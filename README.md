# APARTMENT_API

A .NET 10 Web API for managing apartment buildings and their floors. This project provides RESTful endpoints for authentication, building management, and floor management with JWT-based security.

## 📋 Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Running the Application](#running-the-application)
- [API Documentation](#api-documentation)
- [Project Structure](#project-structure)
- [Database](#database)
- [Authentication](#authentication)
- [Contributing](#contributing)
- [License](#license)

## ✨ Features

- **User Authentication**: JWT-based authentication system
- **Building Management**: Create, read, update, and delete building information
- **Floor Management**: Manage floors within buildings with bilingual support (English and Khmer)
- **OpenAPI/Swagger Documentation**: Interactive API documentation
- **Centralized Exception Handling**: Middleware-based error handling
- **Data Transfer Objects (DTOs)**: Separation of request/response models
- **Repository Pattern**: Clean data access layer
- **AutoMapper**: Automatic model mapping between entities and DTOs
- **Entity Framework Core**: ORM for database operations with Oracle

## 🛠️ Tech Stack

- **.NET Framework**: .NET 10.0
- **Database**: Oracle Database
- **Authentication**: JWT Bearer Tokens
- **ORM**: Entity Framework Core 10.0.9
- **API Documentation**: NSwag 14.7.1 & Scalar
- **Dependency Injection**: Built-in Microsoft DI Container
- **Mapping**: AutoMapper 12.0.1
- **JSON Serialization**: Newtonsoft.Json 13.0.4

## 📦 Prerequisites

- .NET 10.0 SDK or later
- Oracle Database (or compatible)
- Visual Studio 2022 / Visual Studio Code
- Git

## 🚀 Installation

1. **Clone the repository**

   ```bash
   git clone https://github.com/yourusername/APARTMENT_API.git
   cd APARTMENT_API
   ```

2. **Restore dependencies**

   ```bash
   dotnet restore
   ```

3. **Apply migrations**
   ```bash
   dotnet ef database update
   ```

## ⚙️ Configuration

### Database Connection

Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=3333))(CONNECT_DATA=(SID=xe)));User Id=demo_usr;Password=123;"
  }
}
```

### JWT Configuration

Configure JWT settings in `appsettings.json`:

```json
{
  "JWT": {
    "ValidAudience": "http://localhost:5191",
    "ValidIssuer": "http://localhost:5191",
    "Secret": "MySuperSecretKey123456789bbu123456789"
  }
}
```

**⚠️ Security Note**: Change the JWT secret key in production and use secure configuration management (Azure Key Vault, AWS Secrets Manager, etc.).

### Development Configuration

For development-specific settings, modify `appsettings.Development.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## 🏃 Running the Application

### Using .NET CLI

```bash
dotnet run
```

The application will start on `https://localhost:5191` by default.

### Using Visual Studio

1. Open `APARTMENT_API.slnx` in Visual Studio
2. Press `F5` to start debugging
3. The application will open in your default browser

### Using Visual Studio Code

```bash
dotnet watch run
```

## 📖 API Documentation

Once the application is running, access the interactive API documentation:

- **Swagger UI**: `https://localhost:5191/swagger`
- **Scalar UI**: `https://localhost:5191/scalar`
- **OpenAPI JSON**: `https://localhost:5191/openapi/v1.json`

## 📁 Project Structure

```
APARTMENT_API/
├── Controllers/              # API endpoint definitions
│   ├── AuthController.cs
│   ├── BuildingController.cs
│   ├── FloorController.cs
│   └── WeatherForecastController.cs
├── Models/                   # Entity models
│   ├── Building.cs
│   ├── Floor.cs
│   └── User.cs
├── DTOs/                     # Data Transfer Objects
│   ├── Request/
│   │   ├── AuthReqDto.cs
│   │   ├── BuildingReqDto.cs
│   │   └── FloorReqDto.cs
│   └── Response/
│       ├── AuthResDto.cs
│       ├── BuildingResDto.cs
│       └── FloorResDto.cs
├── Services/                 # Business logic
│   ├── Interfaces/
│   └── AuthorizationService.cs, BuildingService.cs, FloorService.cs
├── Repositories/             # Data access layer
│   ├── Interfaces/
│   └── AuthorizationRepository.cs, BuildingRepository.cs, FloorRepository.cs
├── Configurations/           # Setup and configuration
│   ├── ApplicationDbContext.cs
│   └── AutoMapperConfiguration.cs
├── Middlewares/              # Custom middleware
│   └── ExceptionMiddleware.cs
├── Exceptions/               # Custom exception classes
├── Helpers/                  # Utility classes
├── Migrations/               # EF Core migrations
├── Program.cs               # Application startup configuration
├── appsettings.json         # Configuration settings
└── APARTMENT_API.csproj     # Project file
```

## 🗄️ Database

### Models

The application manages the following entities:

- **User**: User accounts for authentication
- **Building**: Apartment buildings (supports bilingual names: English and Khmer)
- **Floor**: Floors within buildings

### Migrations

Database migrations are located in the `Migrations/` folder. To create a new migration:

```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

## 🔐 Authentication

The API uses JWT (JSON Web Tokens) for authentication:

1. Call the authentication endpoint with credentials
2. Receive a JWT token in the response
3. Include the token in the `Authorization` header for protected endpoints:
   ```
   Authorization: Bearer <your_jwt_token>
   ```

Protected endpoints require the `[Authorize]` attribute.

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 📞 Support

For issues, questions, or suggestions, please open an issue on GitHub.

---

**Last Updated**: September 2026
