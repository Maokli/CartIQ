# 🧪 Testing Pyramid

Phase 3 (paired with [[Rich Domain Model]]) — then maintained forever. There are currently **zero** .NET test projects and only Angular smoke specs.

## Why this epic (learning goal)

Learn the test strategy that makes clean architecture pay off: pure domain logic tested in milliseconds, handlers tested against fakes, the whole HTTP surface tested against a real database in containers, and architecture rules enforced by tests instead of code review. The rich-domain refactor is the perfect first customer — write the tests as you refactor.

## Key concepts to learn

- xUnit fundamentals; test naming and Arrange/Act/Assert discipline
- Testing aggregates/value objects: state-based tests, asserting raised domain events
- Handler tests: faking repository abstractions, asserting `Result` outcomes
- `WebApplicationFactory` integration tests + **Testcontainers** (real PostgreSQL, spun up per test run)
- Architecture tests (NetArchTest / ArchUnitNET): dependency rules as executable spec
- Angular component testing with Vitest: signals, `input()`, fixture change detection in zoneless mode

## Tasks

- [ ] **`CartIQ.Domain.UnitTests`** — first .NET test project. Cover: `Money` arithmetic/equality, Receipt aggregate invariants, `FileMetadata` legal/illegal transitions, events raised by behavior methods. *AC: every [[Rich Domain Model]] task lands with tests.*
- [ ] **`CartIQ.Application.UnitTests`** — handler tests with in-memory fakes of `IReceiptsRepository` etc.; FluentValidation validator tests; a test proving `ValidationBehaviour` short-circuits. *AC: Upload/Update/Delete/Get handlers covered incl. `Failure` paths.*
- [ ] **`CartIQ.ArchitectureTests`** — rules: Domain references nothing; Application doesn't reference Infrastructure/WebApi; only Infrastructure touches EF Core. *AC: moving a class to the wrong layer fails the build.* (Module-boundary rules are added in [[Modular Monolith]].)
- [ ] **`CartIQ.WebApi.IntegrationTests`** — `WebApplicationFactory` + Testcontainers-PostgreSQL (sequenced after the Postgres switch in [[Modular Monolith]]; until then, run against a temp SQLite file). Cover: upload→state flow with a stubbed `IOcrService`/`IReceiptParser`, receipts CRUD, ProblemDetails contract. *AC: `dotnet test` green with Docker running, no shared state between tests.*
- [ ] **Angular: real component tests** — receipts-list (paging, loading, empty state), nutrient-bar (computed segments from `input.required`). Replace generated smoke specs with behavioral ones.
- [ ] **Angular: use-case tests** — `ReceiptUseCaseImpl` against a mocked `ReceiptRepository`: Result unwrapping, error propagation.
- [ ] **One command runs everything** — `dotnet test` at solution level + `ng test`; document in README. (CI wiring stays in Ideas/Later.)

## Definition of done

The pyramid exists and is lopsided the right way: many domain/application unit tests, fewer integration tests, architecture tests guarding the layers. A deliberately introduced layering violation or broken invariant fails `dotnet test`.

## Resources

- [Testcontainers for .NET](https://dotnet.testcontainers.org/)
- [Integration tests in ASP.NET Core (learn.microsoft.com)](https://learn.microsoft.com/aspnet/core/test/integration-tests)
- [NetArchTest](https://github.com/BenMorris/NetArchTest)
- [Angular testing guide (angular.dev)](https://angular.dev/guide/testing)
