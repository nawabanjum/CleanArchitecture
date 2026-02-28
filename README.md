# Portfolio Builder Platform

A full-featured **Portfolio Builder API** built with **.NET 9** and **Clean Architecture** principles. Users can create professional portfolios with education, experience, projects, skills, certifications, and social links, then share them publicly via a unique slug URL.

---

## Tech Stack

| Technology | Purpose |
|---|---|
| .NET 9 | Runtime & Web Framework |
| PostgreSQL 18 | Database |
| Entity Framework Core 9 | ORM & Migrations |
| MediatR | CQRS & Mediator Pattern |
| FluentValidation | Request Validation |
| AutoMapper | Object-to-Object Mapping |
| ASP.NET Identity | User Management |
| JWT Bearer Tokens | Authentication |
| Serilog | Structured Logging |
| Swagger / Swashbuckle | API Documentation |

---

## Architecture

The solution follows **Clean Architecture** with strict dependency flow:

```
API (Presentation) --> Application --> Domain (innermost, no dependencies)
Infrastructure     --> Application --> Domain
```

### Solution Structure

```
CleanArchitecture/
├── global.json
├── .gitignore
├── CleanArchitecture.sln
├── src/
│   ├── Portfolio.Domain/           # Entities, Enums, Interfaces (zero dependencies)
│   ├── Portfolio.Application/      # CQRS Commands/Queries, DTOs, Validators, Mappings
│   ├── Portfolio.Infrastructure/   # EF Core, Repositories, Identity, JWT
│   └── Portfolio.API/              # Controllers, Middleware, Program.cs
└── tests/
    ├── Portfolio.Domain.Tests/
    ├── Portfolio.Application.Tests/
    └── Portfolio.API.Tests/
```

---

## Domain Entities

| Entity | Key Fields |
|---|---|
| **ApplicationUser** | extends IdentityUser - FirstName, LastName |
| **Portfolio** | UserId, Title, Slug (unique), Summary, IsPublic |
| **Education** | PortfolioId, Degree, FieldOfStudy, Institution, StartDate, EndDate |
| **Experience** | PortfolioId, JobTitle, Company, Location, StartDate, EndDate, IsCurrent |
| **Project** | PortfolioId, Name, Description, TechStack (list), LiveUrl, GithubUrl, ImageUrl |
| **Skill** | PortfolioId, Name, ProficiencyLevel (enum), Category |
| **Certification** | PortfolioId, Name, IssuingOrganization, IssueDate, ExpiryDate, CredentialUrl |
| **SocialLink** | PortfolioId, Platform (enum), Url |

### Enums

- **ProficiencyLevel**: Beginner, Intermediate, Advanced, Expert
- **SocialPlatform**: GitHub, LinkedIn, Twitter, Website, Other

---

## Design Patterns

| Pattern | Implementation |
|---|---|
| **Clean Architecture** | 4-layer separation with strict dependency inversion |
| **CQRS** | Commands and Queries separated per feature using MediatR |
| **Repository Pattern** | `IGenericRepository<T>` + `IPortfolioRepository` |
| **Unit of Work** | `IUnitOfWork` wrapping EF Core `SaveChangesAsync` |
| **MediatR Pipeline** | `ValidationBehavior<TRequest, TResponse>` auto-validates all requests |
| **Global Exception Handling** | Middleware returning RFC 7807 ProblemDetails |
| **Result Pattern** | `Result<T>` wrapper for explicit success/failure |

---

## API Endpoints

### Authentication (No Auth Required)

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/auth/register` | Register a new user |
| POST | `/api/auth/login` | Login and receive JWT token |

### Portfolios (JWT Required unless noted)

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/portfolios` | Get current user's portfolios |
| POST | `/api/portfolios` | Create a new portfolio |
| GET | `/api/portfolios/{id}` | Get portfolio by ID |
| PUT | `/api/portfolios/{id}` | Update portfolio |
| DELETE | `/api/portfolios/{id}` | Delete portfolio |
| GET | `/api/portfolios/share/{slug}` | **Public** - View portfolio by slug |

### Sub-Entity CRUD (JWT Required)

Each sub-entity follows the same pattern under a portfolio:

| Method | Endpoint Pattern | Description |
|---|---|---|
| POST | `/api/portfolios/{id}/{entity}` | Add item to portfolio |
| PUT | `/api/portfolios/{id}/{entity}/{itemId}` | Update item |
| DELETE | `/api/portfolios/{id}/{entity}/{itemId}` | Delete item |

**Entities**: `educations`, `experiences`, `projects`, `skills`, `certifications`, `social-links`

---

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL 18](https://www.postgresql.org/download/)

### Setup

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd CleanArchitecture
   ```

2. **Configure the database connection**

   Edit `src/Portfolio.API/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=PortfolioDB;Username=postgres;Password=YOUR_PASSWORD"
     }
   }
   ```

3. **Build the solution**
   ```bash
   dotnet build
   ```

4. **Run the API**
   ```bash
   dotnet run --project src/Portfolio.API
   ```

   The database is **auto-migrated** on startup -- no manual migration commands needed.

5. **Open Swagger UI**

   Navigate to: [http://localhost:5152/swagger](http://localhost:5152/swagger)

### Quick Test Flow

1. **Register** a new user via `POST /api/auth/register`
   ```json
   {
     "firstName": "Jane",
     "lastName": "Smith",
     "email": "jane@example.com",
     "password": "Password123!"
   }
   ```

2. **Copy the JWT token** from the response

3. **Authorize in Swagger** - Click the "Authorize" button and paste the token

4. **Create a portfolio** via `POST /api/portfolios`
   ```json
   {
     "title": "Jane Smith Portfolio",
     "slug": "jane-smith",
     "summary": "Full-stack developer portfolio",
     "isPublic": true
   }
   ```

5. **View publicly** via `GET /api/portfolios/share/jane-smith` (no auth needed)

6. **Add sub-items** (education, experience, projects, etc.) to your portfolio

---

## Configuration

### JWT Settings (`appsettings.json`)

```json
{
  "JwtSettings": {
    "Key": "YourSuperSecretKeyThatIsAtLeast32Characters!",
    "Issuer": "PortfolioBuilderAPI",
    "Audience": "PortfolioBuilderClient",
    "ExpiryMinutes": 60
  }
}
```

### Logging (Serilog)

Configured with:
- **Console sink** - Output to terminal
- **File sink** - Rolling daily logs in `Logs/log-YYYY-MM-DD.txt`
- Enrichers: LogContext, MachineName, ThreadId

---

## Project Layer Details

### Portfolio.Domain
Pure C# with zero external dependencies. Contains entities, enums, and repository/service interfaces. All entities inherit from `BaseEntity` which provides `Id`, `CreatedAt`, and `UpdatedAt`.

### Portfolio.Application
Contains all business logic organized by feature using CQRS. Each feature has its own `Commands/` and `Queries/` folders with separate Command, Validator, and Handler classes. Also contains DTOs, AutoMapper profiles, custom exceptions, and the FluentValidation pipeline behavior.

### Portfolio.Infrastructure
Implements all interfaces defined in Domain and Application. Includes EF Core `ApplicationDbContext` with Fluent API configurations, repository implementations, ASP.NET Identity setup, and JWT token generation.

### Portfolio.API
The entry point. Contains controllers that delegate to MediatR, global exception handling middleware, Swagger configuration with JWT support, and the full DI wiring in `Program.cs`.

---

## Error Handling

All exceptions are caught by `ExceptionHandlingMiddleware` and returned as RFC 7807 ProblemDetails:

| Exception | HTTP Status |
|---|---|
| `ValidationException` | 400 Bad Request (with field-level errors) |
| `BadRequestException` | 400 Bad Request |
| `NotFoundException` | 404 Not Found |
| `ForbiddenException` | 403 Forbidden |
| Unhandled exceptions | 500 Internal Server Error |

In Development mode, 500 errors include the full exception message and stack trace.
