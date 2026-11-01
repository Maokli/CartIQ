# 📊 Insights & Signals Dashboard

Phase 6 — the Angular signals deep dive, with the Insights module as its backend.

## Why this epic (learning goal)

The client already uses `signal`/`computed`/`effect`/`input.required`, but data access is still RxJS `HttpClient.subscribe()` with a hand-rolled `loading` signal and an `effect()` that triggers reloads — the exact patterns Angular's resource APIs replace. This epic migrates to signal-native async state and builds the dashboard, the most state-heavy UI in the app, on top of it.

## Key concepts to learn

- `httpResource()` / `rxResource()`: declarative async state (value/status/error) driven by signal params
- `linkedSignal` for resettable state derived from other signals (page ↔ filters)
- `computed()` chains vs `effect()` — why effect-writes-state is an anti-pattern
- `model()` and `output()` for component contracts
- Read models on the backend: aggregation endpoints shaped for the UI, cross-module reads via contracts

## Tasks

### Backend (Insights module)

- [ ] **Aggregation queries** — total spend per week/month, spend per category, **protein per €** (the signature metric), top purchased products, most expensive items. Read-only module consuming Receipts/Catalog contracts (see [[Modular Monolith]]). *AC: each endpoint returns UI-shaped DTOs, no client-side aggregation.*
- [ ] **Insights endpoints** — `GET /api/insights/summary?period=…`, `/api/insights/categories`, `/api/insights/top-products`; paging where relevant.
- [ ] **(Stretch) materialized read model** — project `ReceiptCreated`/`ProductMatched` integration events into a denormalized insights table instead of querying live — a taste of CQRS read models.

### Frontend (signals migration + dashboard)

- [ ] **Migrate data layer to `httpResource()`** — repositories expose resources/signals instead of Observables; delete the manual `loading` signal + reload-`effect()` in `receipts-list`. *AC: no `.subscribe()` left in receipt features; loading/error states come from resource status.*
- [ ] **Pagination via `linkedSignal`** — page resets when filters change; page + filter signals feed the resource request. *AC: changing period filter resets to page 1 without an `effect()`.*
- [ ] **Dashboard page** — spend-over-time chart, macro overview (reuse `nutrient-bar`), category breakdown, protein-per-€ ranking; spartan/ui layout + a chart approach (ngx-charts, or hand-rolled SVG for full control). *AC: period filter updates every widget from one source signal.*
- [ ] **Insights panel** — "Top protein sources", "High cost items", "You overspend on category Z" cards.
- [ ] **Component contracts modernized** — dashboard widgets take `input()`/`model()`, emit via `output()`; no constructor-injection-of-state patterns.
- [ ] **Tests** — resource-driven components tested with Vitest (mock backend via `provideHttpClientTesting`); extend [[Testing Pyramid]].

## Definition of done

Dashboard answers "where does my money and protein go?" at a glance; the receipt features contain zero manual subscription management; every piece of derived UI state is a `computed`/`linkedSignal`/resource — no state written from `effect()`.

## Resources

- [Signals guide (angular.dev)](https://angular.dev/guide/signals)
- [httpResource (angular.dev)](https://angular.dev/api/common/http/httpResource)
- [Resource API (angular.dev)](https://angular.dev/guide/signals/resource)
- [linkedSignal (angular.dev)](https://angular.dev/guide/signals/linked-signal)
