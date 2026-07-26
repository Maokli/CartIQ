# 🥦 Product Catalog & Open Food Facts

Phase 4 — attaches real nutrition data to parsed receipt items via the **Open Food Facts** API.

## Why this epic (learning goal)

Learn robust external-API integration: typed HTTP clients, resilience policies (retry/timeout/circuit-breaker), caching, and respecting a public API's rate limits. Plus a genuinely interesting matching problem: mapping cryptic receipt strings ("HÄHNCHENBR. FIL.") to canonical products — with a human-in-the-loop learning loop.

## Key concepts to learn

- `IHttpClientFactory` + typed clients; `Microsoft.Extensions.Http.Resilience` (Polly v8 pipelines)
- `HybridCache` (or `IMemoryCache`) for external lookups; cache stampede protection
- Open Food Facts data model: products, nutriments per 100g, categories taxonomy
- Anti-corruption layer: OFF's schema stays in Infrastructure; domain sees only your model
- Matching strategies: normalization → exact mapping table → search → manual fallback

## Tasks

- [ ] **Entities** — `Category`, `ProductMapping` (normalizedRawName → productId, source: manual|auto), enrich `Product` (barcode, OFF id, per-100g `NutritionFacts`). Aggregate boundaries per [[Rich Domain Model]].
- [ ] **Open Food Facts client** — typed `HttpClient` for product-by-barcode and text search (`/api/v2/product/{barcode}`, search-a-licious); custom User-Agent per OFF rules; resilience pipeline (retry w/ backoff, timeout, circuit breaker). *AC: pulling the plug mid-request degrades gracefully, no unhandled exception.*
- [ ] **Anti-corruption mapping** — OFF response DTOs live in Infrastructure only; translate nutriments → `NutritionFacts` (handle missing fields — OFF data is crowd-sourced and patchy). *AC: product with absent macro data yields explicit "unknown", not zeros.*
- [ ] **Cache OFF lookups** — `HybridCache` keyed by barcode/query; TTL in config. *AC: second lookup of same product makes no HTTP call (assert via logging/test handler).*
- [ ] **Matching service** — pipeline: normalize raw name (case, abbreviations, umlauts) → `ProductMapping` exact hit → OFF search best-match above confidence threshold → unmatched. *AC: unmatched items are flagged, never silently guessed.*
- [ ] **Learning loop** — when the user manually assigns a product, persist a `ProductMapping` so the same raw name auto-matches next time. *AC: re-uploading a receipt with a previously-corrected item auto-matches.*
- [ ] **Manual mapping endpoint** — assign/reassign product or category to a receipt item.
- [ ] **Seed staples** — categories + a handful of products (chicken breast, skyr, tuna, rice…) via the existing SeedData mechanism.
- [ ] **Angular: mapping UI** — unmatched items surfaced for assignment; product search (spartan select/combobox) backed by OFF search; assign category fallback.
- [ ] **Angular: macros display** — per-item macros + receipt totals; extend the existing `nutrient-bar` component; "protein per €" per item as a teaser for [[Insights & Signals Dashboard]].

## Definition of done

Upload a receipt → most items auto-match or are one click from matched → receipt shows total macros and cost from real OFF data; OFF being down degrades to "nutrition unavailable", never a broken page.

## Resources

- [Open Food Facts API docs](https://openfoodfacts.github.io/openfoodfacts-server/api/)
- [Build resilient HTTP apps (learn.microsoft.com)](https://learn.microsoft.com/dotnet/core/resilience/http-resilience)
- [HybridCache (learn.microsoft.com)](https://learn.microsoft.com/aspnet/core/performance/caching/hybrid)
