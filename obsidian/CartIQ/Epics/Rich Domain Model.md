# 🏛️ Rich Domain Model

Phase 3 (with [[Testing Pyramid]]) — DDD tactical patterns applied to code you already have.

## Why this epic (learning goal)

The domain layer is currently anemic: public setters everywhere, an empty `BaseEvent`, a `_domainEvents` collection nothing ever writes to, and a `FileState` lifecycle driven from outside the entity. This epic turns the dead scaffolding into the real thing — the core of the DDD learning goal. Refactoring an anemic model you wrote yourself teaches more than greenfield DDD ever does.

## Key concepts to learn

- Value objects: immutability, value equality, EF Core owned types / `ComplexProperty`
- Aggregates & aggregate roots: invariant boundaries, why items are only mutated through the root
- Encapsulation: private setters, factory methods, intention-revealing method names
- Domain events: raise in the entity, dispatch on `SaveChanges`, handle via MediatR `INotification`
- EF Core `SaveChangesInterceptor` for auditing and event dispatch
- Where business rules do *not* belong (AutoMapper resolvers, endpoint handlers)

## Tasks

- [ ] **`Money` value object** — amount + currency, arithmetic + equality; replaces `double ProductPrice` and the loose currency string on Receipt. *AC: adding EUR to USD throws; EF persists it as an owned/complex type.*
- [ ] **Immutable `NutritionFacts`** — no setters, value equality, factory with validation (non-negative macros). *AC: unit tests prove equality semantics.*
- [ ] **Receipt as aggregate root** — `Items` exposed as `IReadOnlyCollection`; `AddItem` / `RemoveItem` / `UpdateItem` methods on Receipt enforce invariants (no duplicate lines, totals consistent); private setters throughout; EF backing-field configuration. *AC: compiler prevents `receipt.Items.Add(...)` from Application code.*
- [ ] **`FileMetadata` state machine** — `MarkOcrStarted()`, `MarkOcrCompleted()`, `MarkFailed()`, `MarkReceiptCreated()` guarding legal `FileState` transitions. *AC: illegal transition (e.g. `Uploaded → ReceiptCreated`) throws a domain exception; Application code no longer sets `State` directly.*
- [ ] **Concrete domain events** — e.g. `OcrCompletedEvent`, `ReceiptCreatedEvent`, `ReceiptItemsCorrectedEvent` extending `BaseEvent`; entities call `AddDomainEvent(...)` in their behavior methods.
- [ ] **Event dispatch on SaveChanges** — interceptor/override collects events from tracked entities and publishes via MediatR `INotification` after successful save. *AC: first real consumer — receipt creation triggered by `OcrCompletedEvent` handler, replacing the `ISender` command-chaining inside `UploadFileCommandHandler`.*
- [ ] **Auditing interceptor** — `SaveChangesInterceptor` fills `Created/LastModified` on `BaseAuditableEntity` (they're never set today). *AC: every insert/update stamps timestamps without handler code.*
- [ ] **Audit `ExecuteUpdate`/`ExecuteDelete` usage** — `ReceiptsRepository` uses them; they bypass change tracking, so no events and no auditing fire. Replace where events/audit matter; document where bulk ops are the right call.
- [ ] **Relocate business rules from mappers** — `CategoryResolver` / `NutrientsResolver` / `TopItemsResolver` hold real logic inside AutoMapper; move genuine rules into domain/application, keep mappers dumb.

## Definition of done

No public setter on any domain entity; every state change goes through a named method; domain events flow from entity → SaveChanges → MediatR handler in the running OCR pipeline; auditing fields populate themselves. All proven by [[Testing Pyramid]] unit tests.

## Resources

- Eric Evans, *Domain-Driven Design* (the tactical patterns chapters) / Vaughn Vernon, *Implementing DDD*
- [DDD value objects with EF Core owned & complex types (learn.microsoft.com)](https://learn.microsoft.com/ef/core/modeling/owned-entities)
- [EF Core interceptors (learn.microsoft.com)](https://learn.microsoft.com/ef/core/logging-events-diagnostics/interceptors)
- [Backing fields in EF Core (learn.microsoft.com)](https://learn.microsoft.com/ef/core/modeling/backing-field)
