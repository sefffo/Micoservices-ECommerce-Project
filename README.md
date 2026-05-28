# 🛒 Microservices E-Commerce Platform

<div align="center">

![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Clean Architecture](https://img.shields.io/badge/Clean%20Architecture-6C63FF?style=for-the-badge)
![Microservices](https://img.shields.io/badge/Microservices-00D4FF?style=for-the-badge)

A production-ready **Microservices E-Commerce backend** built with **ASP.NET Core**, following **Clean Architecture** principles. The system is split into independent, loosely coupled services that communicate via shared contracts.

</div>

---

## 🏗️ Architecture Overview

The solution is structured into **3 independent microservices**, each living in its own solution and following a strict **Clean Architecture** layer separation:

```
Micoservices-ECommerce-Project/
│
├── DemoECommerce.ProductApiSolution/       # Product Microservice
│   ├── ProductApi.Domain/                  # Entities, interfaces, domain logic
│   ├── ProductApi.Application/             # Use cases, DTOs, service contracts
│   ├── ProductApi.Infrastructure/          # EF Core, repositories, DB context
│   └── ProductApi.Presentation/            # Controllers, middleware, DI config
│
├── DemoECommerce.OrderApiSolution/         # Order Microservice
│   ├── OrderApi.Domain/
│   ├── OrderApi.Application/
│   ├── OrderApi.Infrastructure/
│   └── OrderApi.Presentation/
│
└── DemoECommerce.SharedLibirarySolution/   # Shared Contracts & Utilities
    └── (Common models, interfaces, extensions shared across services)
```

---

## ⚙️ Tech Stack

| Layer | Technology |
|---|---|
| **Framework** | ASP.NET Core (.NET) |
| **Language** | C# |
| **Architecture** | Clean Architecture · Microservices |
| **ORM** | Entity Framework Core |
| **Design Patterns** | Repository · Unit of Work · CQRS · Dependency Injection |
| **API Style** | RESTful Web API |
| **Auth** | JWT Bearer Authentication |
| **Shared Library** | Cross-service contracts and utilities |

---

## 🚀 Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 8+)
- SQL Server or PostgreSQL
- Visual Studio 2022 / Rider / VS Code

### Running a Service

Each microservice is an independent solution. To run the **Product API**:

```bash
cd DemoECommerce.ProductApiSolution
dotnet restore
dotnet build
dotnet run --project ProductApi.Presentation
```

Repeat the same steps for `DemoECommerce.OrderApiSolution`.

### Configuration

Update `appsettings.json` inside the `Presentation` project of each service with your connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ECommerceProductDb;Trusted_Connection=True;"
  },
  "JwtSettings": {
    "Key": "your-secret-key",
    "Issuer": "your-issuer",
    "Audience": "your-audience"
  }
}
```

---

## 📦 Microservices Breakdown

### 🔹 Product API
Handles all product-related operations — CRUD for products, categories, and inventory management. Exposes RESTful endpoints consumed by other services and clients.

### 🔹 Order API
Manages the order lifecycle — order creation, status tracking, and linking orders to products via service-to-service communication.

### 🔹 Shared Library
A NuGet-style shared project containing common models, response wrappers, interfaces, and extension methods reused across all microservices to avoid duplication.

---

## 🧠 Design Principles

- **Clean Architecture** — strict separation of Domain, Application, Infrastructure, and Presentation layers
- **SOLID Principles** — each class has a single responsibility and depends on abstractions
- **Repository + Unit of Work** — data access abstraction for testability and flexibility
- **Shared Contracts** — cross-service models live in a shared library to enforce consistency

---

## 👤 Author

**Saif Lotfy** — Full Stack Engineer | .NET & Node.js | Clean Architecture Advocate

[![LinkedIn](https://img.shields.io/badge/LinkedIn-%230077B5.svg?style=flat&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/seif-lotfy-769451310/)
[![GitHub](https://img.shields.io/badge/GitHub-121011?style=flat&logo=github&logoColor=white)](https://github.com/sefffo)
