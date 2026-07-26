# 🧱 Modular Monolith

Phase 5 — the heart of the backend journey. Restructure into modules with enforced boundaries so [[Microservices Extraction]] becomes a small step instead of a rewrite.

## Why this epic (learning goal)

Learn strategic DDD: bounded contexts, module contracts, integration events vs domain events, and the **outbox pattern** — the exact skills the monolith→microservices migration story is about. The rule of thumb you're practicing: *if you can't build a well-modularized monolith, microservices will only distribute your mess.*

## Key concepts to learn

- Bounded contexts and how to spot seams (Ingestion / Receipts / Catalog / Insights)
- Module public contracts vs internals; `internal` visibility + `InternalsVisibleTo`
- Domain events (in-module, in-transaction) vs integration events (cross-module, after commit)
- Transactional outbox: why "save + publish" is a lie without it
- EF Core multi-provider setup; schema-per-module in one PostgreSQL database
- docker-compose as the local-stack backbone

## Target modules

| Module | Owns | Publishes (integration events) |
|---|---|---|
| **Ingestion** | FileMetadata, OCR, LLM parsing | `ReceiptParsed` |
| **Receipts** | Receipt, ReceiptItem | `ReceiptCreated`, `ReceiptCorrected` |
| **Catalog** | Product, Category, ProductMapping, OFF client | `ProductMatched` |
| **Insights** | aggregations, read models | — (consumer only) |

## Tasks

- [ ] **PostgreSQL in docker-compose** — replace SQLite; resolves the `// TODO: SELECT BASED ON RUNTIME CONFIG` in Infrastructure DI; connection string from environment; re-baseline migrations (dev data is disposable). *AC: `docker compose up` + `dotnet run` works from a clean clone.*
- [ ] **Choose module topology and document it** — folders-per-module inside existing projects vs projects-per-module. Write an ADR (architecture decision record) in obsidian either way. *Recommended: projects-per-module (`CartIQ.Modules.Ingestion` …) — makes boundaries compiler-enforced.*
- [ ] **Carve out the modules** — move feature folders into modules; each module gets `PublicApi`/contracts (DTOs, event records, service interfaces) and internal implementation; each owns its DbContext or schema (`ingestion.*`, `receipts.*`, `catalog.*`).
- [ ] **Enforce boundaries with architecture tests** — extend [[Testing Pyramid]]: no module references another module's internals; only contracts assemblies are shared. *AC: referencing `Ingestion` internals from `Receipts` fails the build/tests.*
- [ ] **Integration events (in-process)** — separate from domain events: a module handles its own domain event and publishes an integration event record on an in-process bus abstraction (`IEventBus`). Receipt creation from `ReceiptParsed` becomes cross-module. *AC: `Ingestion` has no reference to `Receipts` — only to the shared contract.*
- [ ] **Transactional outbox** — integration events written to an `outbox_messages` table in the same transaction as state changes; a `BackgroundService` dispatcher polls, publishes in-process, marks processed, retries failures. *AC: kill the app between save and dispatch → event still delivered after restart; no event ever published for a rolled-back transaction.*
- [ ] **Idempotent consumers** — inbox/processed-message tracking so replayed events don't duplicate receipts. *AC: dispatching the same outbox message twice creates one receipt.*
- [ ] **Compose grows** — `docker-compose.yml`: API + PostgreSQL + Ollama (+ pgAdmin optional). Single `docker compose up` for the full backend stack.

## Definition of done

Solution builds as four modules with compiler/test-enforced boundaries; all cross-module communication goes through contracts + outbox-backed integration events; local stack is one compose command. The codebase is now honestly "distribution-ready" — [[Microservices Extraction]] only swaps the bus.

## Resources

- [Transactional outbox pattern (microservices.io)](https://microservices.io/patterns/data/transactional-outbox.html)
- [.NET microservices architecture e-book — modular monolith & DDD chapters (learn.microsoft.com)](https://learn.microsoft.com/dotnet/architecture/microservices/)
- Milan Jovanović's modular monolith series (search: "modular monolith milan jovanovic") — pragmatic .NET-specific walkthroughs
- Sam Newman, *Monolith to Microservices*
