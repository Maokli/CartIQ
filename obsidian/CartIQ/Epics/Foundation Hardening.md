# 🩹 Foundation Hardening

Phase 1 — do this first. Everything after builds on a skeleton that must be trustworthy.

## Why this epic (learning goal)

Learn what separates a tutorial API from a production-grade one: pipeline-level error handling, validation as a cross-cutting concern, and an error channel that's actually used. Also: fix the real bugs already in the code — debugging your own walking skeleton is the best teacher.

## Key concepts to learn

- ASP.NET Core middleware pipeline & `IExceptionHandler` / ProblemDetails (RFC 9457)
- MediatR pipeline behaviors as the clean-architecture home for cross-cutting concerns
- FluentValidation and where validation belongs (request vs domain invariants)
- Result pattern vs exceptions: when to return `Failure`, when to throw
- Streaming file uploads correctly (bytes, not strings)

## Tasks

- [x] **Fix binary upload corruption** — `DiskFileUploadService` + the Upload endpoint read/write the file as a UTF-8 string; images corrupt. Stream `IFormFile.OpenReadStream()` to disk. *AC: upload a JPEG, byte-for-byte identical on disk (compare checksums).*
- [x] **Validate uploads** — content-type allowlist (jpeg/png/pdf), max size from config. *AC: oversized/wrong-type upload returns 400 ProblemDetails, nothing written to disk.*
- [x] **Implement `RawOcrResultsRepository.AddRawOcrResult`** — currently `throw new NotImplementedException()`. *AC: OCR result row persisted and linked to the file/receipt.*
- [x] **Global exception handling** — `IExceptionHandler` + `AddProblemDetails()`; no raw stack traces to clients. *AC: forced exception returns RFC 9457 body with traceId.*
- [x] **MediatR `ValidationBehaviour`** — add FluentValidation, validators for Update/Upload commands, behavior short-circuits to `Failure`. *AC: invalid UpdateReceipt returns 400 listing each field error.*
- [x] **`LoggingBehaviour` + `UnhandledExceptionBehaviour`** — request name + elapsed ms logged; unhandled exceptions logged with request payload.
- [x] **Transaction around UpdateReceipt** — resolves the `// TODO: Wrap in transaction` (delete items + add items + update are non-atomic today). *AC: induced failure mid-update leaves DB unchanged.*
- [x] **Config hygiene** — CORS origins from config (not `AllowAnyOrigin`), delete leftover `WeatherForecast`, fix copy-pasted OpenAPI summary on Files endpoints, make the SQLite path relative.
- [x] **Angular: register `provideHttpClient()`** — it's missing; `ReceiptRepositoryImpl` would throw NullInjectorError. *AC: receipts list loads against the running API.*
- [x] **Angular: explicit `provideZonelessChangeDetection()`** in `app.config.ts` (currently implicit).
- [ ] **Angular: environment-based API base URL** — replace hard-coded `http://localhost:5200` in `ReceiptRepositoryImpl` with an injected config/environment value.

## Definition of done

Upload → OCR (dummy) → receipt flow works with a real image file; every expected error is a ProblemDetails response; invalid commands never reach a handler body; the Angular app runs against config, not hard-coded URLs.

## Resources

- [Handle errors in ASP.NET Core (learn.microsoft.com)](https://learn.microsoft.com/aspnet/core/fundamentals/error-handling)
- [FluentValidation docs](https://docs.fluentvalidation.net/)
- [MediatR pipeline behaviors wiki](https://github.com/jbogard/MediatR/wiki/Behaviors)
- [Upload files in ASP.NET Core (learn.microsoft.com)](https://learn.microsoft.com/aspnet/core/mvc/models/file-uploads)
