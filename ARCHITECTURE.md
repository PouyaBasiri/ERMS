# ERMS Architecture

## 1. Project Identity

**ERMS** is an enterprise-oriented .NET application built around:

- Clean / Layered Architecture
- Domain-Driven Design (DDD)
- CQRS with MediatR
- Minimal API
- Feature-oriented organization
- EF Core
- PostgreSQL
- Domain Events
- Result / Error pattern

The architecture is designed so that business rules remain independent from delivery mechanisms and infrastructure technologies.

---

## 2. Architectural Layers

```text
ERMS.Api
    |
    v
ERMS.Application
    |
    v
ERMS.Domain
    |
    v
ERMS.SharedKernel

ERMS.Infrastructure
    |
    +----> ERMS.Application
    +----> ERMS.Domain
    +----> ERMS.SharedKernel
```

### ERMS.Api

Presentation layer.

Responsibilities:

- Minimal API endpoints
- HTTP routing
- Request binding
- HTTP response mapping
- Global exception handling
- OpenAPI / API documentation
- Composition root integration

The API must remain thin and must not contain business rules or persistence logic.

### ERMS.Application

Use-case layer.

Responsibilities:

- Commands
- Queries
- Command / Query handlers
- Validators
- Pipeline behaviors
- Application orchestration
- Application-facing contracts

Application coordinates business operations but does not implement infrastructure concerns such as EF Core or PostgreSQL access.

### ERMS.Domain

Business model.

Responsibilities:

- Entities
- Aggregate roots
- Value Objects
- Domain rules
- Domain errors
- Domain events
- Repository abstractions already established by the project

The Domain must not depend on API or Infrastructure implementations.

### ERMS.Infrastructure

Technical implementation layer.

Responsibilities:

- EF Core
- PostgreSQL persistence
- DbContext
- Repository implementations
- Unit of Work
- Persistence configuration
- Value converters
- SaveChanges interceptors
- Domain event dispatch implementation

Infrastructure implements contracts required by the upper layers.

### ERMS.SharedKernel

Shared domain/application primitives that are genuinely common across the system.

Current areas include:

- Entity
- AggregateRoot
- ValueObject
- Errors
- Results
- Domain Events contracts
- Business Rules
- Pagination

SharedKernel must remain small and must not become a dumping ground for application-specific functionality.

---

## 3. Dependency Direction

The dependency direction is non-negotiable:

```text
API -> Application -> Domain -> SharedKernel

Infrastructure -> Application
Infrastructure -> Domain
Infrastructure -> SharedKernel
```

Rules:

- Domain must not reference API.
- Domain must not reference Infrastructure.
- Application must not reference Infrastructure.
- API may reference Application and Infrastructure at the composition boundary.
- Infrastructure provides implementations for abstractions consumed by Application / Domain.

A lower layer must not know about a higher layer.

---

## 4. API Architecture

The API uses Minimal API with endpoint classes.

Current endpoint pattern:

```csharp
public sealed class CreateUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/users", HandleAsync)
            .WithName("CreateUser")
            .WithTags("Users");
    }
}
```

Endpoint registration is centralized through `EndpointExtensions.MapEndpoints()`.

Endpoints are responsible for:

1. Receiving HTTP input.
2. Creating / receiving the application request.
3. Sending the request through MediatR.
4. Converting the application `Result` into an HTTP response.

Endpoints must not contain business logic or direct EF Core access.

---

## 5. Application Architecture

The project uses CQRS.

Each use case is represented by its own command or query.

Example:

```text
Users/
├── CreateUser/
│   ├── CreateUserCommand.cs
│   ├── CreateUserCommandHandler.cs
│   ├── CreateUserResponse.cs
│   └── CreateUserValidator.cs
│
└── GetUser/
    ├── GetUserQuery.cs
    ├── GetUserQueryHandler.cs
    └── GetUserResponse.cs
```

Commands and queries are application requests. They are not domain entities and must not contain business state or infrastructure code.

---

## 6. MediatR Pipeline

Cross-cutting application concerns are handled through pipeline behaviors.

Current behaviors:

```text
LoggingBehavior
ValidationBehavior
PerformanceBehavior
TransactionBehavior
```

Conceptually:

```text
Request
  |
  v
Logging
  |
  v
Validation
  |
  v
Performance
  |
  v
Transaction
  |
  v
Handler
```

The exact registration order is controlled by Application dependency injection and must remain consistent with the intended execution order.

---

## 7. Result / Error Pattern

Application use cases return `Result` or `Result<T>` instead of using exceptions for expected business failures.

```text
Result
├── Success
└── Failure(Error)

Result<T>
├── Success(Value)
└── Failure(Error)
```

`Error` contains:

- Code
- Description
- ErrorType

Current error categories include:

- Failure
- Validation
- NotFound
- Conflict
- Unauthorized
- Forbidden
- Unexpected

API converts results to HTTP responses through a centralized `ResultExtensions` mapping.

---

## 8. Domain Model

### Aggregate Root

Aggregates encapsulate domain state and domain behavior.

The current `AggregateRoot` owns a collection of `IDomainEvent` instances and exposes methods to raise and clear events.

### Value Objects

Current User value objects include:

- `Email`
- `FullName`

Value Objects:

- Have no independent identity.
- Are created through controlled construction / factory methods.
- Encapsulate their own validation and equality semantics.
- Are not exposed as mutable primitives.

### Domain Rules

Business invariants that belong to the domain are represented through domain rules and domain behavior rather than being placed in API endpoints.

---

## 9. Domain Events

Domain Events are part of the domain model and are published after persistence through the infrastructure integration.

Current conceptual flow:

```text
AggregateRoot
    |
    | RaiseDomainEvent(...)
    v
Domain Event Collection
    |
    v
SaveChanges Interceptor
    |
    v
IDomainEventDispatcher
    |
    v
MediatR IPublisher
```

`IDomainEvent` implements `MediatR.INotification` in the current architecture so MediatR can publish the event.

The dispatcher accepts an `IReadOnlyCollection<IDomainEvent>` and publishes each event.

---

## 10. Persistence Architecture

Persistence uses:

- Entity Framework Core 10
- Npgsql
- PostgreSQL

Current infrastructure pieces include:

```text
Persistence/
├── ApplicationDbContext.cs
├── UnitOfWork.cs
├── Configurations/
│   └── UserConfiguration.cs
└── Converters/
    └── EmailConverter.cs
```

Repositories are implemented in Infrastructure.

Repository abstractions already established in the project must be reused; duplicate abstractions for the same responsibility must not be introduced.

---

## 11. EF Core Mapping Rules

The current User mapping follows these patterns:

- `Email` uses an EF Core `ValueConverter`.
- `FullName` uses `OwnsOne`.
- Domain Events are not persisted.
- Persistence configuration is isolated from the Domain model.

The Domain model must not reference EF Core types.

---

## 12. Unit of Work

`IUnitOfWork` is defined outside Infrastructure and implemented by Infrastructure.

Repositories are responsible for manipulating entities through the persistence abstraction.

The Unit of Work is responsible for committing changes through `SaveChangesAsync`.

This keeps persistence coordination outside individual domain entities and API endpoints.

---

## 13. Exception Handling

Unhandled exceptions are centralized through ASP.NET Core's exception handling pipeline.

Current mechanism:

```text
Unhandled Exception
       |
       v
GlobalExceptionHandler
       |
       v
ProblemDetails
```

Expected business failures should normally use `Result` / `Error` rather than exceptions.

Unexpected technical failures are handled centrally.

---

## 14. API Response Mapping

Application results are converted to HTTP responses in the API layer.

The Endpoint should not duplicate HTTP error mapping logic.

Central mapping currently covers:

```text
Validation   -> 400
NotFound     -> 404
Conflict     -> 409
Unauthorized -> 401
Forbidden    -> 403
Unexpected   -> 500
```

Success responses depend on the returned `Result` type and endpoint semantics.

---

## 15. Configuration and Technology Boundary

Infrastructure is the technology boundary for persistence.

The current database provider is PostgreSQL.

Connection strings belong to the host configuration and are consumed by Infrastructure during composition.

The Domain and Application layers must not contain PostgreSQL connection details or EF Core provider-specific code.

---

## 16. Non-Negotiable Rules

### 16.1 Compile Consistency

Every new type must compile against the actual signatures and dependencies already present in the project.

Before introducing a class:

- Verify existing interfaces.
- Verify existing return types.
- Verify generic parameters.
- Verify namespaces.
- Verify package versions.
- Verify constructors.

A new class must not assume an older or imagined version of an existing class.

### 16.2 Consistency

Existing finalized classes and interfaces are the source of truth.

Rules:

- Reuse existing abstractions before creating new ones.
- Do not create parallel interfaces for the same responsibility.
- Do not change an established signature merely to fit a new implementation.
- Do not introduce a second architectural pattern for the same problem.
- Do not move completed classes between layers without an explicit architecture decision.

### 16.3 Dependency Direction

Never introduce a dependency that violates the layer direction.

In particular:

```text
Application  -X-> Infrastructure
Domain       -X-> Infrastructure
Application  -X-> API
Domain       -X-> API
Domain       -X-> ASP.NET Core
```

Infrastructure may depend inward to implement required abstractions.

---

## 17. Architectural Change Policy

An architectural change must be explicitly declared before implementation.

Examples:

- Introducing a new abstraction that replaces an existing one.
- Moving an interface between projects.
- Replacing a persistence pattern.
- Changing the endpoint registration pattern.
- Changing the CQRS / MediatR strategy.
- Changing the Result / Error contract.

Such changes must be treated as an explicit architecture decision, not as an incidental part of a feature.

Until such a decision is approved, the existing architecture is considered frozen.

---

## 18. Development Workflow

Each phase follows this sequence:

```text
Define phase
   ↓
Check existing architecture/contracts
   ↓
Implement only required changes
   ↓
dotnet build
   ↓
Fix compile/runtime issues
   ↓
Commit
   ↓
Next phase
```

A phase is not considered complete until the project builds successfully.

---

## 19. Current Functional Flow

The first implemented functional flow is Create User.

```text
POST /api/users
      |
      v
CreateUserEndpoint
      |
      v
CreateUserCommand
      |
      v
MediatR
      |
      v
Pipeline Behaviors
      |
      v
CreateUserCommandHandler
      |
      +----> Value Objects
      |
      +----> Domain Rules
      |
      +----> User Aggregate
      |
      +----> Repository
      |
      v
UnitOfWork / EF Core
      |
      v
PostgreSQL
      |
      v
Domain Event Dispatch
```

The Get User query is implemented using the existing `IUserReadRepository` abstraction and follows the same dependency direction.

---

## 20. Architecture Status

Current status:

- Clean / Layered structure established.
- Domain model established.
- SharedKernel established.
- CQRS established.
- MediatR pipeline established.
- Result / Error pattern established.
- EF Core persistence established.
- PostgreSQL connected.
- Domain Events established.
- Minimal API established.
- Create User end-to-end flow completed.
- Get User query in progress.

The architecture is considered **frozen for feature development** unless an explicit architectural decision is made.
