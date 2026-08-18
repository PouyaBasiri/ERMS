# ERMS

Enterprise Resource Management System built with modern .NET architecture.

## Overview

ERMS is being developed as an enterprise-oriented application platform with a layered architecture, Domain-Driven Design (DDD), CQRS, MediatR, Minimal API, Entity Framework Core, and PostgreSQL.

The project is designed so that business logic remains independent from infrastructure and presentation concerns, while cross-cutting concerns are handled consistently through the application pipeline.

## Current Architecture

```text
ERMS
│
├── src
│   ├── ERMS.Api
│   ├── ERMS.Application
│   ├── ERMS.Contracts
│   ├── ERMS.Domain
│   ├── ERMS.Infrastructure
│   └── ERMS.SharedKernel
│
└── docs
    └── adr
```

### Dependency Direction

```text
ERMS.Api
    ↓
ERMS.Application
    ↓
ERMS.Domain
    ↓
ERMS.SharedKernel

ERMS.Infrastructure
    ↓
ERMS.Application
    ↓
ERMS.Domain
    ↓
ERMS.SharedKernel
```

The dependency direction is intentional. Application must not depend on Infrastructure, and Domain must not depend on API or Infrastructure.

## Architectural Style

The current project combines:

- Clean / Layered Architecture
- Domain-Driven Design (DDD)
- CQRS
- MediatR
- Minimal API
- Feature-oriented organization
- Repository and Unit of Work patterns
- Domain Events
- Result Pattern

## Projects

### ERMS.SharedKernel

Contains shared domain primitives used across the solution:

- Entity
- AggregateRoot
- ValueObject
- Result / Result<T>
- Error / ErrorType
- Business Rules
- Domain Event abstractions
- Pagination primitives

SharedKernel must remain independent from the higher application and infrastructure layers.

### ERMS.Domain

Contains the business domain:

- Aggregates
- Entities
- Value Objects
- Domain Rules
- Domain Errors
- Domain Events
- Repository abstractions

Current main aggregate:

```text
Users
```

The `User` aggregate currently uses:

- `Email` Value Object
- `FullName` Value Object
- `IsActive` state
- Domain behavior such as Activate, Deactivate, and ChangeEmail
- `UserCreatedEvent`

### ERMS.Application

Contains use cases and application orchestration:

- CQRS commands and queries
- MediatR handlers
- FluentValidation
- Pipeline Behaviors
- Application-level orchestration

Current use case:

```text
CreateUser
```

Current query:

```text
GetUser
```

### ERMS.Infrastructure

Contains technical implementations:

- EF Core
- PostgreSQL provider
- DbContext
- Repository implementations
- Unit of Work
- Domain Event Dispatcher
- EF Core interceptors
- Entity configurations
- Value converters
- Database migrations

### ERMS.Api

Contains the HTTP presentation layer:

- Minimal API endpoints
- Routing
- ProblemDetails / exception handling
- Result to HTTP mapping
- OpenAPI / Scalar integration
- Composition root and dependency registration

## Current Request Flow

A typical command flows through the application as follows:

```text
HTTP Request
    ↓
Minimal API Endpoint
    ↓
MediatR
    ↓
Logging Behavior
    ↓
Validation Behavior
    ↓
Performance Behavior
    ↓
Transaction Behavior
    ↓
Command Handler
    ↓
Domain
    ↓
Repository
    ↓
EF Core
    ↓
PostgreSQL
```

For domain events:

```text
Aggregate
    ↓
Domain Event
    ↓
Domain Events Interceptor
    ↓
IDomainEventDispatcher
    ↓
MediatR Publisher
    ↓
Domain Event Handlers
```

## Current Features

### Create User

Endpoint:

```text
POST /api/users
```

The current flow includes:

- Request validation
- Email Value Object creation and validation
- FullName Value Object creation and validation
- Email uniqueness rule
- User aggregate creation
- Repository persistence
- PostgreSQL persistence
- Domain Event publication
- Result to HTTP response mapping

### Get User

Endpoint:

```text
GET /api/users/{id}
```

The current query flow includes:

- Query creation
- MediatR dispatch
- Read repository
- User lookup
- `Result<GetUserResponse>` response
- HTTP mapping

## Persistence

The current database provider is PostgreSQL.

Technology stack:

```text
.NET 10
EF Core 10
Npgsql
PostgreSQL
```

EF Core migrations are stored in:

```text
ERMS.Infrastructure/Migrations
```

Initial migration:

```text
InitialCreate
```

## Cross-Cutting Concerns

### Validation

Implemented through FluentValidation and a MediatR pipeline behavior.

### Logging

Implemented through a MediatR pipeline behavior and global exception handling.

### Performance

Implemented through a MediatR pipeline behavior for request execution timing.

### Transaction / Unit of Work

Persistence is coordinated through `IUnitOfWork` and EF Core `SaveChangesAsync`.

### Exception Handling

The API uses a global exception handler and ProblemDetails for unhandled exceptions.

### Result Mapping

Application results are converted into HTTP responses through a centralized API extension.

## Domain Modeling Rules

Value Objects are used for business concepts instead of primitive-only representations.

Examples:

```text
Email
FullName
```

Value Objects are created through factory methods and encapsulate their own invariants.

The `User` aggregate exposes behavior instead of relying on public setters.

## Development Rules

### Compile Consistency

Every implementation must be compatible with the actual versions and signatures already present in the project.

Before introducing a new type, existing types and abstractions must be checked first.

### Consistency

Existing finalized classes are the source of truth.

Do not create duplicate abstractions for an existing responsibility.

Examples:

```text
Do not create a second IUserReadRepository.
Do not create a second Result abstraction.
Do not create parallel Endpoint registration mechanisms.
```

### Dependency Direction

Higher-level business layers must not depend on lower-level technical implementations.

```text
API → Application → Domain → SharedKernel
Infrastructure → Application / Domain / SharedKernel
```

### Architecture Changes

Architectural changes must be explicitly identified before implementation.

A feature phase must not silently introduce a different architecture, folder strategy, abstraction, or dependency model.

## Git Workflow

Feature development is performed on feature branches.

Recommended commit pattern:

```text
feat(...): ...
refactor(...): ...
fix(...): ...
```

Each completed phase should build successfully before moving to the next phase.

## Current Development Status

Completed:

```text
Sprint 1  — Solution / foundational structure
Sprint 2  — SharedKernel / DDD foundations
Sprint 3  — Application / CQRS / Behaviors
Sprint 4  — EF Core / PostgreSQL / Persistence
Sprint 5  — API / CreateUser / GetUser / End-to-End flow
```

Current status:

```text
Sprint 6 — Query / Read-side development
```

## Next Planned Work

The next work continues with the read side and API verification before moving to authentication and authorization.

## Architecture Documentation

The architecture description should live separately from this operational README.

Planned document:

```text
ARCHITECTURE.md
```

Architecture decisions should be recorded under:

```text
docs/adr/
```

## License

Internal / project-specific. No public license has been defined yet.
