<div align="center">

# 🛵 Talabat Delivery

### Food Delivery E-Commerce REST API Built with ASP.NET Core 9.0

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-239120?style=flat-square&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-EF%20Core%209-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/sql-server)
[![Redis](https://img.shields.io/badge/Redis-Basket%20%26%20Cache-DC382D?style=flat-square&logo=redis&logoColor=white)](https://redis.io/)
[![Stripe](https://img.shields.io/badge/Stripe-Payment%20Intents-635BFF?style=flat-square&logo=stripe&logoColor=white)](https://stripe.com/)
[![JWT](https://img.shields.io/badge/Auth-JWT%20%2B%20Identity-000000?style=flat-square&logo=jsonwebtokens&logoColor=white)](https://jwt.io/)
[![Clean Architecture](https://img.shields.io/badge/Architecture-Clean-blue?style=flat-square)](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](http://makeapullrequest.com)

**[Features](#-features) • [Installation](#-quick-start) • [Documentation](#-api-documentation) • [Contact](#-contact)**

---

A food-delivery backend that covers the whole ordering journey: browse a menu, keep a basket, sign in, pay by card with **Stripe**, and place orders. Built with **Clean Architecture**, the **Repository / Unit of Work / Specification** patterns, **Redis** for baskets and response caching, and **JWT** authentication.

</div>

---

## 📋 Table of Contents

- [✨ Features](#-features)
- [🏗️ Architecture](#️-architecture)
- [🚀 Quick Start](#-quick-start)
- [🔄 Checkout Flow](#-checkout-flow)
- [📚 API Documentation](#-api-documentation)
- [🔐 Authentication](#-authentication)
- [⚠️ Error Handling](#️-error-handling)
- [🛠️ Tech Stack](#️-tech-stack)
- [📊 Design Patterns](#-design-patterns)
- [🎯 Key Highlights](#-key-highlights)
- [🧩 Customize](#-customize)
- [🤝 Contributing](#-contributing)
- [📫 Contact](#-contact)

---

## ✨ Features

<table>
<tr>
<td width="50%">

### 🔐 Authentication & Accounts
- **JWT Bearer authentication** (token valid for 2 days)
- **ASP.NET Core Identity** with a dedicated identity database
- **Roles** seeded on startup (Admin, SuperAdmin)
- **Email check**, **current user** and **saved address** endpoints
- Claims inside the token: email, username, user id, roles
- Issuer, audience, lifetime and signing key validation

### 🍽️ Menu & Products
- **18 seeded dishes** with images served from `wwwroot`
- **Brands** (food categories): Chicken, Seafood, Pasta and Noodles, Soup, Desserts, Vegetable
- **Types** (cuisines): Italian, Mexican, Indian, Chinese
- **Filter** by brand and type
- **Case-insensitive search** by name
- **Sort** by name or price (asc / desc)
- **Pagination** (default 5, max 10 per page)

</td>
<td width="50%">

### 🛒 Basket, Orders & Payments
- **Redis basket** with a 5-hour expiry
- **Order creation** straight from the basket
- **4 delivery methods** (UPS1, UPS2, UPS3, FREE)
- **Order statuses**: Pending, PaymentReceived, PaymentFailed
- **Stripe Payment Intents** (card)
- **Server-side totals**: prices are re-read from the database

### ⚡ Technical Excellence
- **Clean Architecture** across 6 projects
- **Repository, Unit of Work & Specification** patterns
- **`[Cache]` action filter** backed by Redis (90s default)
- **Global exception middleware** with consistent JSON
- **Custom validation responses** per field
- **Swagger UI** with a Bearer **Authorize** button
- **Auto migrations & seeding** on startup

</td>
</tr>
</table>

### 🧾 Feature Details

| Area | What it does |
| --- | --- |
| Products | List with filter, search, sort and pagination. Brands, types and single product endpoints. List response is cached in Redis |
| Basket | Stored as JSON in Redis under a client-chosen key. Create, update, get and delete. Holds delivery method, shipping price, payment intent id and client secret |
| Auth | Register, login, JWT issuing with role claims, email check, current user |
| Address | One saved address per user. Created on first update, edited afterwards |
| Orders | Built from the basket. Items store a snapshot (product id, name, picture) with the price taken from the database. Total is subtotal plus delivery price |
| Payments | Creates a Stripe Payment Intent the first time, updates its amount when the basket changes. Currency is USD, card only |
| Idempotent orders | If an order already exists for the same payment intent, it is replaced instead of duplicated |
| Pictures | Picture URLs are built dynamically from the `Urls:BaseUrl` setting through AutoMapper resolvers |
| Errors | Unknown endpoints, validation failures, not found, unauthorized and unexpected errors all return the same JSON shape |
| Database | Two SQL Server databases (store and identity), each with its own migrations. Entity rules are set in `IEntityTypeConfiguration` classes |
| Seeding | Brands, types, products and delivery methods come from JSON files. Roles and sample users are created for Identity |
| CORS | A single policy that only allows the configured frontend origin |

---

## 🏗️ Architecture

This project follows **Clean Architecture**. Business rules live in the core and never depend on the database, the web layer or Stripe.

```text
┌─────────────────────────────────────────────────────────────┐
│                     Host / Presentation                      │
│  ┌──────────────────────┐    ┌──────────────────────────┐  │
│  │   Talabat Delivery   │    │  ECommerce.Presentation  │  │
│  │ Program.cs, Middleware│   │  Controllers, [Cache]    │  │
│  └──────────────────────┘    └──────────────────────────┘  │
└─────────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────────┐
│                      Business Layer                          │
│  ┌─────────────────────────────────────────────────────┐   │
│  │               ECommerce.Service                      │   │
│  │  • AuthenticationService  • ProductServices          │   │
│  │  • BasketServices         • OrderService             │   │
│  │  • PaymentService         • CacheService             │   │
│  │  • ServiceManager         • Specifications           │   │
│  └─────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────────┐
│                       Domain Layer                           │
│  ┌──────────────────────┐    ┌──────────────────────────┐  │
│  │   ECommerce.Domain   │    │  ECommerce.Abstraction   │  │
│  │ Models, Exceptions,  │    │  Service interfaces      │  │
│  │ Repo/UOW contracts   │    │  (IServiceManager ...)   │  │
│  └──────────────────────┘    └──────────────────────────┘  │
└─────────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────────┐
│                  Infrastructure Layer                        │
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────┐        │
│  │ SQL Server  │  │    Redis    │  │   Stripe    │        │
│  │ (2 DbContexts)│ │(Basket+Cache)│ │ (Payments)  │        │
│  └─────────────┘  └─────────────┘  └─────────────┘        │
└─────────────────────────────────────────────────────────────┘
```

`ECommerce.Shared` (DTOs, error models, query params, pagination) is used by all layers.

### 📁 Project Structure

```text
Talabat-Delivery/
│
├── Core/
│   ├── ECommerce.Domain/              # Domain layer
│   │   ├── Models/                    # Identity, Orders, Products, Baskets
│   │   ├── Exceptions/                # NotFound, BadRequest, UnAuthorized ...
│   │   └── Contratcs/                 # Repos, UOW, Specifications, Seed
│   │
│   ├── ECommerce.Abstraction/         # Service interfaces
│   │   └── IServices/
│   │
│   └── ECommerce.Service/             # Business logic layer
│       ├── Services/                  # Auth, Product, Basket, Order, Payment, Cache
│       ├── Specifications/            # Product, Order, Count specifications
│       └── MappingProfiles/           # AutoMapper profile & picture resolvers
│
├── ECommerce.Shared/                  # Shared layer
│   ├── Dtos/                          # Basket, Identity, Order, Product DTOs
│   ├── ErrorModels/                   # Error & validation responses
│   └── Common/                        # ProductQueryParams, sorting, pagination
│
├── Infrastructure/
│   ├── ECommerce.Persistence/         # Data access layer
│   │   ├── Contexts/                  # StoreDbContext, StoreIdentityDbContext
│   │   ├── Repos/                     # Generic, Basket (Redis), Cache (Redis)
│   │   ├── Configurations/            # EF Core entity configurations
│   │   ├── Migrations/                # Store migrations
│   │   ├── Identity/Migrations/       # Identity migrations
│   │   ├── Seed/ + Data/              # DataSeeding and JSON seed files
│   │   ├── UOW/                       # Unit of Work
│   │   └── SpecificationEvaluator.cs
│   │
│   └── ECommerce.Presentation/        # API layer
│       ├── Controllers/               # Authentication, Product, Basket, Order, Payment
│       └── Attributes/                # [Cache] action filter
│
└── Talabat Delivery/                  # API host
    ├── Custom Middlewares/            # Global exception middleware
    ├── wwwroot/images/products/       # Product images
    └── Program.cs                     # Entry point & service registration
```

---

## 🚀 Quick Start

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/sql-server) (LocalDB/Express/Full)
- [Redis](https://redis.io/download) (Windows: [Redis on Windows](https://github.com/microsoftarchive/redis/releases))
- [Stripe Account](https://stripe.com) (test keys are enough)

### Installation

1️⃣ **Clone the repository**
```bash
git clone https://github.com/BaherElgobashi/Talabat-Delivery.git
cd Talabat-Delivery
```

2️⃣ **Configure application settings**

Add the following to `appsettings.Development.json` inside the **Talabat Delivery** project:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TalabatStore;Trusted_Connection=true;",
    "IdentityConnection": "Server=(localdb)\\mssqllocaldb;Database=TalabatIdentity;Trusted_Connection=true;",
    "RedisConnection": "localhost:6379"
  },
  "JWTOptions": {
    "Issuer": "TalabatAPI",
    "Audience": "TalabatClients",
    "SecurityKey": "YourSuperSecretKeyHere_MinimumLengthRequired"
  },
  "StripeSettings": {
    "SecretKey": "sk_test_your_key"
  },
  "Urls": {
    "BaseUrl": "https://localhost:7119/"
  },
  "FrontendUrl": "http://localhost:4200"
}
```

| Setting | Purpose |
| --- | --- |
| `ConnectionStrings:DefaultConnection` | Store database (products, orders, delivery methods) |
| `ConnectionStrings:IdentityConnection` | Identity database (users, roles, addresses) |
| `ConnectionStrings:RedisConnection` | Redis server for baskets and response cache |
| `JWTOptions:*` | Issuer, audience and signing key for tokens |
| `StripeSettings:SecretKey` | Stripe secret key used by the payment service |
| `Urls:BaseUrl` | Prefix added to product picture paths (keep the trailing `/`) |
| `FrontendUrl` | The only origin allowed by the CORS policy |

> ⚠️ Never commit real secrets. Use [User Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets) or environment variables.

3️⃣ **Start Redis**
```bash
redis-server
```

4️⃣ **Run the API**
```bash
cd "Talabat Delivery"
dotnet run
```

> Pending migrations are applied and the databases are seeded **automatically** on startup. Run the project from the `Talabat Delivery` folder so the seed files can be found.

5️⃣ **Open Swagger**
- URL: `https://localhost:7119/swagger` (Development environment)
- Click **Authorize** and paste: `Bearer {token}`

### 🌱 Seed Data

| Data | Content |
| --- | --- |
| Brands | Chicken, Seafood, Pasta and Noodles, Soup, Desserts, Vegetable |
| Types | Italian, Mexican, Indian, Chinese |
| Products | 18 dishes with pictures |
| Delivery methods | UPS1 (10, 1-2 Days), UPS2 (5, 2-5 Days), UPS3 (2, 5-10 Days), FREE (0, 1-2 Weeks) |
| Identity | Admin and SuperAdmin roles with sample users |

---

## 🔄 Checkout Flow

```text
1. Register / Login            →  receive a JWT
2. Browse the menu             →  GET  /api/Product/Products
3. Build the basket            →  POST /api/Basket
4. Pick a delivery method      →  GET  /api/Order/DeliveryMethods
                                  (set deliveryMethodId inside the basket)
5. Create the payment intent   →  POST /api/Payment/{basketId}
                                  (returns clientSecret for the frontend)
6. Place the order             →  POST /api/Order
7. Track your orders           →  GET  /api/Order/AllOrders
```

The order endpoint needs a basket that already has a payment intent, so payment creation always comes before order creation.

---

## 📚 API Documentation

> All endpoints are available in **Swagger UI** (Development environment). Below are the key examples.

### 🧭 Endpoints at a Glance

| Controller | Method | Route | Auth |
| --- | --- | --- | :---: |
| Authentication | POST | `/api/Authentication/Login` | ❌ |
| Authentication | POST | `/api/Authentication/Register` | ❌ |
| Authentication | GET | `/api/Authentication/CheckEmail?Email=` | ✅ |
| Authentication | GET | `/api/Authentication/CurrentUser` | ✅ |
| Authentication | GET | `/api/Authentication/Address` | ✅ |
| Authentication | PUT | `/api/Authentication/Address` | ✅ |
| Product | GET | `/api/Product/Products` | ✅ |
| Product | GET | `/api/Product/Brands` | ✅ |
| Product | GET | `/api/Product/Types` | ✅ |
| Product | GET | `/api/Product/{id}` | ✅ |
| Basket | GET | `/api/Basket?Key=` | ❌ |
| Basket | POST | `/api/Basket` | ❌ |
| Basket | DELETE | `/api/Basket/{id}?Key=` | ❌ |
| Order | POST | `/api/Order` | ✅ |
| Order | GET | `/api/Order?OrderId=` | ✅ |
| Order | GET | `/api/Order/AllOrders` | ✅ |
| Order | GET | `/api/Order/DeliveryMethods` | ✅ |
| Payment | POST | `/api/Payment/{basketId}` | ✅ |

### 🔑 Authentication

#### Register
```http
POST /api/Authentication/Register
Content-Type: application/json

{
  "email": "user@example.com",
  "userName": "johndoe",
  "displayName": "John Doe",
  "password": "SecurePass123!",
  "phoneNumber": "+201234567890"
}
```

#### Login
```http
POST /api/Authentication/Login
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "SecurePass123!"
}
```

**Response:**
```json
{
  "email": "user@example.com",
  "displayName": "John Doe",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

#### Update Address
```http
PUT /api/Authentication/Address
Authorization: Bearer {token}
Content-Type: application/json

{
  "firstName": "John",
  "lastName": "Doe",
  "street": "12 Tahrir Street",
  "city": "Cairo",
  "country": "Egypt"
}
```

---

### 🍽️ Products

#### Get All Products (Filtered, Sorted, Paginated)
```http
GET /api/Product/Products?BrandId=1&TypeId=3&SearchValue=chicken&sortingWay=3&PageIndex=1&PageSize=5
Authorization: Bearer {token}
```

| Parameter | Description |
| --- | --- |
| `BrandId` | Filter by brand (food category) |
| `TypeId` | Filter by type (cuisine) |
| `SearchValue` | Search inside the product name |
| `sortingWay` | `1` NameAsc, `2` NameDesc, `3` PriceAsc, `4` PriceDesc |
| `PageIndex` | Page number (default `1`) |
| `PageSize` | Items per page (default `5`, max `10`) |

**Response:**
```json
{
  "pageSize": 2,
  "pageIndex": 1,
  "totalCount": 2,
  "data": [
    {
      "id": 17,
      "name": "Tandoori Chicken",
      "description": "...",
      "pictureUrl": "https://localhost:7119/images/products/TandooriChicken.png",
      "brandName": "Chicken",
      "typeName": "Indian",
      "price": 150
    },
    {
      "id": 18,
      "name": "Indian Chicken Tikka Masala",
      "description": "...",
      "pictureUrl": "https://localhost:7119/images/products/ChickenTikkaMasala.jpg",
      "brandName": "Chicken",
      "typeName": "Indian",
      "price": 180
    }
  ]
}
```

> `pageSize` in the response is the number of items actually returned on that page. `totalCount` is the number of products matching the filters.

---

### 🛒 Basket

#### Create / Update Basket
```http
POST /api/Basket
Content-Type: application/json

{
  "id": "basket-abc123",
  "items": [
    {
      "id": 1,
      "name": "Italian Chicken Marinade",
      "pictureUrl": "images/products/ItalianChickenMarinade.png",
      "price": 200,
      "quantity": 2
    }
  ],
  "deliveryMethodId": 1
}
```

Validation: `quantity` must be between 1 and 100, `price` must be at least 1.

#### Get / Delete Basket
```http
GET    /api/Basket?Key=basket-abc123
DELETE /api/Basket/{id}?Key=basket-abc123
```

---

### 📦 Orders

#### Create Order
```http
POST /api/Order
Authorization: Bearer {token}
Content-Type: application/json

{
  "basketId": "basket-abc123",
  "deliveryMethodId": 1,
  "address": {
    "firstName": "John",
    "lastName": "Doe",
    "street": "12 Tahrir Street",
    "city": "Cairo",
    "country": "Egypt"
  }
}
```

**Response:**
```json
{
  "id": "3f2c9a52-8b1e-4a7e-9d7e-5c1f6d0b9a11",
  "userEmail": "user@example.com",
  "orderDate": "2026-10-28T12:00:00+03:00",
  "deliveryMethod": "UPS1",
  "orderState": "Pending",
  "address": {
    "firstName": "John",
    "lastName": "Doe",
    "street": "12 Tahrir Street",
    "city": "Cairo",
    "country": "Egypt"
  },
  "items": [
    {
      "productName": "Italian Chicken Marinade",
      "productPictureUrl": "https://localhost:7119/images/products/ItalianChickenMarinade.png",
      "quantity": 2,
      "price": 200
    }
  ],
  "subTotal": 400,
  "total": 410
}
```

#### Other Order Endpoints
```http
GET /api/Order/DeliveryMethods
GET /api/Order/AllOrders          # newest first
GET /api/Order?OrderId={guid}
```

---

### 💳 Payments

#### Create / Update Payment Intent
```http
POST /api/Payment/{basketId}
Authorization: Bearer {token}
```

**Response (the basket with payment data):**
```json
{
  "id": "basket-abc123",
  "items": [ ... ],
  "deliveryMethodId": 1,
  "shippingPrice": 10,
  "paymentIntentId": "pi_xxx",
  "clientSecret": "pi_xxx_secret_yyy"
}
```

How the amount is calculated:
1. Each basket item's price is replaced with the current price from the database
2. The delivery method price is added
3. The total is converted to the smallest currency unit and sent to Stripe

---

## 🔐 Authentication

### JWT Authentication
- Stateless, token-based authentication
- Tokens expire after **2 days**
- Claims: email, username, user id and one claim per role
- Signed with **HMAC-SHA256**
- Validated on every request: issuer, audience, lifetime and signing key

### Protected Endpoints

| Controller | Access |
| --- | --- |
| `Authentication` | Login and Register are public. CheckEmail, CurrentUser and Address need a token |
| `Product` | Needs a token |
| `Order` | Needs a token |
| `Payment` | Needs a token |
| `Basket` | Public |

### Using the token in Swagger
1. Call `Login` or `Register` and copy the `token`
2. Click **Authorize** at the top of Swagger UI
3. Enter `Bearer {token}` and confirm

---

## ⚠️ Error Handling

A global middleware catches every exception and returns the same JSON shape.

| Exception | HTTP Status |
| --- | :---: |
| `UnAuthorizedException` | 401 |
| `BadRequestException` | 400 |
| `NotFoundException` (product, basket, user, address, delivery method) | 404 |
| Any other exception | 500 |
| Unknown endpoint | 404 |

**General error:**
```json
{
  "statusCode": 404,
  "message": "Product With Id 99 is not found"
}
```

**Validation error:**
```json
{
  "statusCode": 400,
  "message": " Validation Error",
  "validationErrors": [
    {
      "fields": "Email",
      "errors": ["The Email field is not a valid e-mail address."]
    }
  ]
}
```

---

## 🛠️ Tech Stack

### Backend Framework
- **ASP.NET Core 9.0** - Web API framework
- **Entity Framework Core 9** - ORM, migrations and entity configurations
- **ASP.NET Core Identity** - User and role management

### Database & Caching
- **SQL Server** - Two databases: store and identity
- **Redis (StackExchange.Redis)** - Basket storage and response caching

### Authentication & Security
- **JWT Bearer Tokens** - API authentication
- **Identity password hashing** - Handled by ASP.NET Core Identity
- **CORS** - Limited to the configured frontend URL

### Payment Processing
- **Stripe.net** - Payment Intents for card payments

### Development Tools
- **AutoMapper** - Entity and DTO mapping
- **Swashbuckle** - Swagger / OpenAPI with Bearer security
- **ILogger** - Error logging inside the exception middleware

---

## 📊 Design Patterns

| Pattern | Purpose | Location |
| --- | --- | --- |
| **Repository** | Abstract data access (generic, Redis basket, Redis cache) | `ECommerce.Domain/Contratcs/Repos`, `ECommerce.Persistence/Repos` |
| **Unit of Work** | Hand out repositories and save changes together | `ECommerce.Domain/Contratcs/UOW`, `ECommerce.Persistence/UOW` |
| **Specification** | Reusable query rules: filters, includes, ordering, paging | `ECommerce.Service/Specifications`, `SpecificationEvaluator` |
| **Service Manager** | One entry point to all services with lazy creation | `ECommerce.Service/Services/ServiceManager.cs` |
| **Action Filter** | Declarative response caching with `[Cache]` | `ECommerce.Presentation/Attributes` |
| **Middleware** | Global exception handling | `Talabat Delivery/Custom Middlewares` |
| **Dependency Injection** | Loose coupling between layers | `Program.cs` |

---

## 🎯 Key Highlights

### 🏆 Clean Architecture Benefits
- **Testability**: Business logic does not depend on EF Core, Redis or Stripe
- **Maintainability**: Each project has one clear job
- **Flexibility**: Infrastructure can be swapped without touching the core

### ⚡ Performance Optimizations
- **Redis basket** for fast reads and writes
- **`[Cache]` filter** serves repeated product queries straight from Redis
- **Pagination** keeps responses small
- **Async/await** across the whole request path

### 🔒 Security & Reliability
- Protected endpoints with `[Authorize]` and full JWT validation
- Prices are **never trusted from the client**: payment and order totals use database prices
- Orders store a snapshot of product name, picture and price
- Replaying order creation for the same payment intent replaces the old order instead of duplicating it
- Consistent error responses for every failure type
- CORS restricted to a single frontend origin

### 🧰 Developer Experience
- One command to run: migrations and seeding happen on startup
- Swagger with built-in Bearer authorization
- Settings-driven picture URLs, no hard-coded hosts

---

## 🧩 Customize

| I want to... | Where |
| --- | --- |
| Change page size limits | `ProductQueryParams` (`DefaultSize`, `MaximumSize`) |
| Add a sort option | `ProductSortingWay` enum and `ProductSpecifications` |
| Change cache duration | `[Cache(120)]` on the action (seconds, default 90) |
| Change basket expiry | `BasketRepository.CreateUpdateBasketAsync` (default 5 hours) |
| Change token lifetime | `AuthenticationService.CreateTokenAsync` (default 2 days) |
| Add products, brands, types | `Infrastructure/ECommerce.Persistence/Data/*.json` |
| Add a delivery method | `delivery.json` |
| Add a new service | Interface in `ECommerce.Abstraction`, implementation in `ECommerce.Service`, expose it in `ServiceManager` |
| Allow another frontend | `FrontendUrl` setting |

---

## 🤝 Contributing

Contributions, issues, and feature requests are welcome!

1. Fork the repository
2. Create your feature branch: `git checkout -b feature/AmazingFeature`
3. Commit your changes: `git commit -m "Add some AmazingFeature"`
4. Push to the branch: `git push origin feature/AmazingFeature`
5. Open a Pull Request

---

## 📫 Contact

<div align="center">

[![GitHub](https://img.shields.io/badge/GitHub-BaherElgobashi-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/BaherElgobashi)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Baher_Elgobashi-0A66C2?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/baher-elgobashi-1975a5298)
[![Gmail](https://img.shields.io/badge/Gmail-bahermohamedelgobashi@gmail.com-D14836?style=for-the-badge&logo=gmail&logoColor=white)](mailto:bahermohamedelgobashi@gmail.com)

**Made with ❤️ by Baher Mohamed**

⭐ Star this repo if you find it useful!

</div>
