![.NET Core](https://img.shields.io/badge/.NET%20Core-v.7-purple)
![EF Core](https://img.shields.io/badge/EF%20Core-v.7-cyan)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-v.14-drakblue)

# Fullstack Project

## Table of Contents

## Table of Contents

1. [Project Overview](#project-overview)
   - [Frontend](#frontend)
   - [Backend](#backend)
   - [Swagger Documentation](#swagger-documentation)
   - [Technologies](#technologies)
2. [Docker](#docker)
3. [GitHub Actions Workflow](#github-actions-workflow)
4. [Features](#features)
   - [User Management](#user-management)
   - [Admin Functionalities](#admin-functionalities)
5. [Project Setup Instructions](#project-setup-instructions)
6. [Testing](#testing)
7. [Architecture](#architecture)
8. [Database Design](#database-design)
9. [Endpoints](#endpoints)
   - [Authentication](#authentication)
   - [Category](#category)
   - [Order](#order)
   - [Product](#product)
   - [Review](#review)
   - [User](#user)
10. [Demo](#demo)

## Project Overview

### Frontend

The frontend of the project has been deployed and is accessible in the repository at [https://github.com/hasanmd91/.NET_fullstack_project_frontend](https://github.com/hasanmd91/.NET_fullstack_project_frontend): Live Link of the frontend Project [https://ilhshoestore.netlify.app/](https://ilhshoestore.netlify.app/)

### Backend

This backend project is build wtih ASP.NET Core, Entity Framework Core, and PostgreSQL and is deployed on Microsoft Azure. The backend provides endpoints for performing CRUD operations based on Authorization. Backend deployed link [https://ecommershop.azurewebsites.net](https://ecommershop.azurewebsites.net)

### Swagger Documentation

[https://ecommershop.azurewebsites.net](https://ecommershop.azurewebsites.net)

### Technologies

- ASP.Net Core
- Entity Framework Core
- PostgreSql
- Xunit
- Microsoft Azure

## Docker

To use the pre-built Docker image for this project, you can pull it from Docker Hub. Run the following command:

```
docker pull hasanmd91/ilhbackend
```

Once the image is pulled, you can run the Docker container using the following command:

```
docker run -p 8080:8080 hasanmd91/ilhbackend
```

Alternatively, you can run the docker-compose.yml file, you can use the following command to build and start the container

```
docker-compose up --build
```

## GitHub Actions Workflow

The CI/CD pipeline is defined in the `.github/workflows` directory and it consists of the following stages:

- **Build:** Build the project and dependencies.
- **Test:** Run unit tests and any other relevant tests.
- **Docker:** Build and push the docker image to the docker hub.

## Features

### User Management:

- Users can register for an account and log in.
- Users can view all available products and individual product details.
- Users can post, delete and update products review.
- The ability to search and sort products is supported.
- Users can add products to their shopping cart.
- Cart management functionality is available.
- Users can order product by checkout no payment info is required

### Admin Functionalities

- Admins have the ability to view and delete user accounts.
- Admins can view, edit, delete, and add new products.
- Admins can view all orders and update order status
- Admins can update a user to admin.

## Project Setup Instructions

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

## Testing

- To run all the tests, use the following command in the Test directory of the solution:

```
dotnet test

```

## Architecture

![Architecture](Design/architecture.png)

## DataBase Design

![DataBase Design](Design/erd.png)

## Endpoints

#### Authentication

| Method | Path                |
| ------ | ------------------- |
| POST   | `/api/auth/login`   |
| GET    | `/api/auth/profile` |

#### Category

| Method | Path                         |
| ------ | ---------------------------- |
| GET    | `/api/category`              |
| POST   | `/api/category`              |
| GET    | `/api/category/{categoryId}` |
| DELETE | `/api/category/{categoryId}` |
| PATCH  | `/api/category/{categoryId}` |

#### Order

| Method | Path                   |
| ------ | ---------------------- |
| GET    | `/api/order`           |
| POST   | `/api/order`           |
| GET    | `/api/order/{orderId}` |
| DELETE | `/api/order/{orderId}` |
| PATCH  | `/api/order/{orderId}` |

#### Product

| Method | Path                       |
| ------ | -------------------------- |
| GET    | `/api/product`             |
| POST   | `/api/product`             |
| GET    | `/api/product/{productId}` |
| DELETE | `/api/product/{productId}` |
| PATCH  | `/api/product/{productId}` |

#### Review

| Method | Path                     |
| ------ | ------------------------ |
| POST   | `/api/review`            |
| DELETE | `/api/review/{reviewId}` |
| PATCH  | `/api/review/{reviewId}` |

#### User

| Method | Path                 |
| ------ | -------------------- |
| GET    | `/api/user`          |
| POST   | `/api/user`          |
| GET    | `/api/user/{userId}` |
| PATCH  | `/api/user/{userId}` |
| DELETE | `/api/user/{userId}` |

## Demo

[![Youtube Demo](https://img.youtube.com/vi/kPy1wzJwmSU/0.jpg)](https://www.youtube.com/watch?v=kPy1wzJwmSU)
