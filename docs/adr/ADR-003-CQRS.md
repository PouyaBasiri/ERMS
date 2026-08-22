ADR-003-CQRS.md

Status: Accepted

Decision:
Use CQRS with MediatR for application use cases.

Why:
- Isolate use cases
- Enable pipeline behaviors
- Keep handlers focused

Consequences:
- Commands/Queries are separate
- MediatR is an Application dependency