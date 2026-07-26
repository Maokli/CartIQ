---

kanban-plugin: board

---

## 🧠 Ideas / Later

- [ ] Authentication & per-user data (ASP.NET Identity + JWT)
- [ ] Observability: Serilog + OpenTelemetry + local Grafana/Jaeger stack
- [ ] CI: GitHub Actions — build, test, lint API + client
- [ ] Barcode scanning support (feeds [[Product Catalog & Open Food Facts]] by-barcode lookup)
- [ ] Fuzzy matching upgrade: string similarity / embeddings for product matching
- [ ] Experiment: vision LLM reads receipt image directly (skip Tesseract)
- [ ] Knapsack-style constraint solver for the optimizer
- [ ] Improve OCR accuracy (preprocessing: deskew, contrast)
- [ ] Optimize DB queries (indexes, compiled queries) once Insights is real
- [ ] UI/UX polish pass + basic wireframes for remaining pages
- [ ] Retro: "what production would still need" (service discovery, secrets, deployment) [[Microservices Extraction]]


## 🩹 Phase 1 — Harden the Skeleton

- [ ] Fix binary upload corruption (stream bytes, not strings) [[Foundation Hardening]]
- [ ] Validate uploads: content-type + size limits [[Foundation Hardening]]
- [ ] Implement RawOcrResultsRepository.AddRawOcrResult [[Foundation Hardening]]
- [ ] Global exception handling + ProblemDetails [[Foundation Hardening]]
- [ ] Use Result.Failure and map to HTTP status codes [[Foundation Hardening]]
- [ ] FluentValidation + MediatR ValidationBehaviour [[Foundation Hardening]]
- [ ] Logging + UnhandledException behaviours [[Foundation Hardening]]
- [ ] Wrap UpdateReceipt in a transaction [[Foundation Hardening]]
- [ ] Config hygiene: CORS from config, remove WeatherForecast, fix Files OpenAPI text [[Foundation Hardening]]
- [ ] Angular: add missing provideHttpClient() [[Foundation Hardening]]
- [ ] Angular: explicit provideZonelessChangeDetection() [[Foundation Hardening]]
- [ ] Angular: environment-based API base URL [[Foundation Hardening]]


## 🤖 Phase 2 — Receipt Pipeline + Local LLM

- [ ] docker-compose with Ollama (start of the local stack) [[Local LLM Receipt Parsing]]
- [ ] Pick local model + write decision note (accuracy/latency/RAM on 3 receipts) [[Local LLM Receipt Parsing]]
- [ ] Real OCR: Tesseract behind IOcrService, delete DummyOCRService [[Local LLM Receipt Parsing]]
- [ ] LLM parsing service: OCR text → structured items via Microsoft.Extensions.AI [[Local LLM Receipt Parsing]]
- [ ] Prompt as versioned file (incl. filter rules: totals, VAT, deposit lines) [[Local LLM Receipt Parsing]]
- [ ] Handle garbage LLM output: retry once, then OcrError + manual review [[Local LLM Receipt Parsing]]
- [ ] True background processing: Channel + BackgroundService [[Local LLM Receipt Parsing]]
- [ ] Drive FileState machine end-to-end, persisted per transition [[Local LLM Receipt Parsing]]
- [ ] Store parsed receipt + items from LLM output [[Local LLM Receipt Parsing]]
- [ ] Angular: upload UI (drag-drop, progress) [[Local LLM Receipt Parsing]]
- [ ] Angular: processing status via polling resource() (stretch: SSE) [[Local LLM Receipt Parsing]]
- [ ] Angular: manual correction UI (edit names, delete wrong items) [[Local LLM Receipt Parsing]]


## 🏛️ Phase 3 — Rich Domain + Tests

- [ ] Money value object (replaces double price + currency string) [[Rich Domain Model]]
- [ ] Immutable NutritionFacts with value equality [[Rich Domain Model]]
- [ ] Receipt as aggregate root: items only via root methods, private setters [[Rich Domain Model]]
- [ ] FileMetadata state machine: guarded FileState transitions [[Rich Domain Model]]
- [ ] Concrete domain events raised in entities [[Rich Domain Model]]
- [ ] Dispatch events on SaveChanges → MediatR (first consumer: receipt creation from OcrCompleted) [[Rich Domain Model]]
- [ ] Auditing SaveChangesInterceptor for BaseAuditableEntity [[Rich Domain Model]]
- [ ] Audit ExecuteUpdate/ExecuteDelete + move rules out of AutoMapper resolvers [[Rich Domain Model]]
- [ ] CartIQ.Domain.UnitTests (first .NET test project) [[Testing Pyramid]]
- [ ] CartIQ.Application.UnitTests (handlers + validators) [[Testing Pyramid]]
- [ ] CartIQ.ArchitectureTests (layer rules) [[Testing Pyramid]]
- [ ] WebApi integration tests (WebApplicationFactory, Testcontainers after Postgres) [[Testing Pyramid]]
- [ ] Angular: behavioral component + use-case tests [[Testing Pyramid]]


## 🥦 Phase 4 — Products & Open Food Facts

- [ ] Entities: Category, ProductMapping, enriched Product [[Product Catalog & Open Food Facts]]
- [ ] Research + integrate Open Food Facts API (typed HttpClient, resilience pipeline) [[Product Catalog & Open Food Facts]]
- [ ] Anti-corruption layer: OFF nutriments → NutritionFacts, handle patchy data [[Product Catalog & Open Food Facts]]
- [ ] Cache OFF lookups (HybridCache) [[Product Catalog & Open Food Facts]]
- [ ] Matching service: normalize → mapping table → OFF search → manual [[Product Catalog & Open Food Facts]]
- [ ] Learning loop: manual assignments become ProductMappings [[Product Catalog & Open Food Facts]]
- [ ] Manual mapping endpoint [[Product Catalog & Open Food Facts]]
- [ ] Seed staple categories + products (chicken, skyr, tuna, rice…) [[Product Catalog & Open Food Facts]]
- [ ] Angular: assign product/category UI with OFF search [[Product Catalog & Open Food Facts]]
- [ ] Angular: macros per item + receipt totals (extend nutrient-bar) [[Product Catalog & Open Food Facts]]


## 🧱 Phase 5 — Modular Monolith

- [ ] PostgreSQL in docker-compose, drop SQLite, re-baseline migrations [[Modular Monolith]]
- [ ] ADR: module topology (folders vs projects per module) [[Modular Monolith]]
- [ ] Carve out modules: Ingestion, Receipts, Catalog, Insights — contracts vs internals [[Modular Monolith]]
- [ ] Schema-per-module in Postgres [[Modular Monolith]]
- [ ] Architecture tests enforce module boundaries [[Modular Monolith]]
- [ ] In-process integration events between modules (IEventBus) [[Modular Monolith]]
- [ ] Transactional outbox + background dispatcher [[Modular Monolith]]
- [ ] Idempotent consumers (inbox / processed-message tracking) [[Modular Monolith]]


## 📊 Phase 6 — Insights & Signals

- [ ] Aggregations: spend per week, per category, protein per €, top products, priciest items [[Insights & Signals Dashboard]]
- [ ] Insights endpoints (UI-shaped read DTOs) [[Insights & Signals Dashboard]]
- [ ] Angular: migrate data layer to httpResource(), kill manual loading/effect [[Insights & Signals Dashboard]]
- [ ] Angular: pagination + filters via linkedSignal [[Insights & Signals Dashboard]]
- [ ] Dashboard: spend chart, macro overview, category breakdown [[Insights & Signals Dashboard]]
- [ ] Insights panel: top protein sources, high-cost items, overspend hints [[Insights & Signals Dashboard]]
- [ ] Widgets use input()/model()/output() contracts [[Insights & Signals Dashboard]]
- [ ] Stretch: event-projected read model (CQRS taste) [[Insights & Signals Dashboard]]


## 🛰️ Phase 7 — Extract Microservices

- [ ] RabbitMQ in compose (management UI + healthchecks) [[Microservices Extraction]]
- [ ] MassTransit in the monolith: outbox dispatcher → broker [[Microservices Extraction]]
- [ ] CartIQ.Contracts package + versioning rule [[Microservices Extraction]]
- [ ] Extract Ingestion worker service (own DB, consumes FileUploaded, publishes ReceiptParsed) [[Microservices Extraction]]
- [ ] MinIO object storage replaces disk file store [[Microservices Extraction]]
- [ ] Failure semantics: retries, dead-letter queue, fault consumer → OcrError [[Microservices Extraction]]
- [ ] Prove distributed idempotency (redelivery → one result) [[Microservices Extraction]]
- [ ] Full-stack compose: API + worker + Postgres + RabbitMQ + MinIO + Ollama [[Microservices Extraction]]
- [ ] Stretch: YARP gateway [[Microservices Extraction]]


## ⚙️ Phase 8 — Optimization Engine

- [ ] Optimization service in its own module (reads Catalog + Insights contracts)
- [ ] Baseline algorithm: rank products by protein/€, fill until budget
- [ ] Input form: budget, target calories, target protein
- [ ] Generate optimized shopping list with total cost + macros
- [ ] Suggestions: "Replace X with Y", "You overspend on category Z"
- [ ] Compare optimized list vs actual purchase history


## ✅ Done

- [x] Setup ASP.NET solution (API, Application, Domain, Infrastructure)
- [x] Setup database + EF Core
- [x] Create core entities (Receipt, ReceiptItem, Product)
- [x] Implement basic CRUD for receipts
- [x] Setup Angular project (standalone, zoneless, signals)
- [x] Create Angular layout (navbar, routing)
- [x] Migrate to spartan/ui (remove legacy design system + wrapper components)
- [x] Decide DB: PostgreSQL (switch scheduled in Phase 5)
- [x] Decide OCR approach: Tesseract + local LLM parsing (Phase 2)
- [x] Define core value proposition (grocery spend + nutrition optimizer)
- [x] Define macro schema (protein, carbs, fats, kcal → NutritionFacts)
- [x] File upload endpoint (WIP on features/fileUpload — hardening in Phase 1)




%% kanban:settings
```
{"kanban-plugin":"board","list-collapse":[false]}
```
%%
