# 🛰️ Microservices Extraction

Phase 7 — the payoff. Deliberately last: extraction is cheap *because* [[Modular Monolith]] made boundaries, contracts, and the outbox real first.

## Why this epic (learning goal)

Learn distributed systems on a system you fully understand: message brokers, consumer semantics (at-least-once, idempotency, poison messages), shared-nothing storage, and the operational cost of the network hop. Ingestion is the natural first extraction — it's async, compute-heavy (OCR + LLM), and already communicates only via events.

## Key concepts to learn

- RabbitMQ topology: exchanges, queues, dead-letter exchanges
- MassTransit: consumers, retry/redelivery policies, fault handling, test harness
- Outbox → broker: swapping the in-process dispatcher for real transport
- At-least-once delivery ⇒ idempotent consumers (the inbox pattern earns its keep)
- Shared-nothing: object storage (MinIO/S3 API) instead of shared disk; DB-per-service
- Contracts as versioned packages; consumer-driven compatibility

## Tasks

- [ ] **RabbitMQ in compose** — with management UI; healthchecks so dependents wait.
- [ ] **Introduce MassTransit in the monolith first** — point the outbox dispatcher at RabbitMQ instead of the in-process bus; all modules still in one process, but events now cross a real broker. *AC: watch `ReceiptParsed` flow through the management UI; system behavior unchanged.*
- [ ] **Contracts package** — integration event records into `CartIQ.Contracts` (project reference locally; mentally a nuget package). Document a simple versioning rule (additive changes only).
- [ ] **Extract `CartIQ.Ingestion.Worker`** — standalone .NET worker service: consumes `FileUploaded`, runs OCR + LLM parse, publishes `ReceiptParsed`. Own PostgreSQL database/schema, own migrations, own DI. Delete the Ingestion module from the monolith. *AC: monolith has no OCR/LLM/Tesseract/Ollama dependency left.*
- [ ] **MinIO for file storage** — replace `DiskFileUploadService` with S3-API object storage in compose; API uploads to bucket, worker reads from bucket. *AC: API and worker containers share no volume.*
- [ ] **Failure semantics** — MassTransit retry policy (exponential backoff), then dead-letter queue; `OcrError` state set via a fault consumer; poison message doesn't block the queue. *AC: kill Ollama mid-parse → message retries, eventually dead-letters, file ends in `OcrError`, queue keeps flowing.*
- [ ] **Idempotency proven distributed** — redeliver a consumed `FileUploaded` → exactly one parse result. (Inbox pattern from [[Modular Monolith]] carried over.)
- [ ] **Full-stack compose** — API + ingestion worker + PostgreSQL + RabbitMQ + MinIO + Ollama; one command, healthcheck-ordered startup; Angular dev server against it. *AC: clean-clone `docker compose up` → upload a receipt photo → parsed receipt appears.*
- [ ] **(Stretch) YARP gateway** — single entry point routing to API (+ future services); teaches reverse-proxy config.
- [ ] **Retro note: "what production would still need"** — service discovery, observability (OpenTelemetry), secrets management, deployment. Feeds the Ideas column — knowing what you *didn't* build is part of the learning.

## Definition of done

Two independently-runnable .NET services communicating only via RabbitMQ contracts and sharing no storage; a receipt photo uploaded in the browser is parsed by the worker and appears in the app; killing either service mid-flight loses no data.

## Resources

- [MassTransit docs](https://masstransit.io/documentation/concepts)
- [RabbitMQ tutorials](https://www.rabbitmq.com/tutorials)
- [.NET microservices e-book (learn.microsoft.com)](https://learn.microsoft.com/dotnet/architecture/microservices/)
- Sam Newman, *Monolith to Microservices* (extraction patterns)
