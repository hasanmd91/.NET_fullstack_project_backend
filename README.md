# Fullstack Project

![.NET Core](https://img.shields.io/badge/.NET%20Core-v.7-purple)
![EF Core](https://img.shields.io/badge/EF%20Core-v.7-cyan)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-v.14-drakblue)

### Frontend

### Backend

## Table of Contents

1. [Technologies](#technologies)
1. [Project Setup Instructions](#projectsetupinstructions)

### Technologies

- ASP.Net Core
- Entity Framework Core
- PostgreSql
- Xunit

### Project Setup Instructions

- Clone the Project
- Ensure that all required packages in every layer are installed according to the .csproj files. Navigate to each layer and execute:

```
dotnet restore
```

- Create a local appsettings.json file in the root directory of the project. You can use the provided template below and adjust it as necessary:

```
{
  "ConnectionStrings": {
    "localDb": "YOUR_DATABASE_CONNECTION_STRING_HERE"
  },
  "Jwt": {
    "Issuer": "YOUR_ISSUER_VALUE",
    "Audience": "YOUR_AUDIENCE_VALUE",
    "Key": "YOUR_SECRET_KEY_VALUE"
  }
}

```

- Insert your local database connection string under the "localDb" key in the appsettings.json file.
- Provide appropriate values for Issuer, Audience, and Key under the Jwt section in the appsettings.json file.
- Execute Entity Framework (EF) Core migrations to update the database schema. Navigate to the project containing the DbContext and execute:

```
dotnet ef migrations add <Migration_Name>
dotnet ef database update

```

- Navigate to the Ecom.WebAPI folder and run the project using either of the following commands:

```
dotnet run

```

### Testing

- To run all the tests, use the following command in the Test directory of the solution:

```
dotnet test

```
