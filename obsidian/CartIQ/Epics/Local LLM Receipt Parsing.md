# 🤖 Local LLM Receipt Parsing

Phase 2 — lands the `features/fileUpload` branch properly. This is the start of the local stack.

## Why this epic (learning goal)

Learn to run and integrate a **local LLM** as an application component: model selection, structured output, prompt versioning, and defensive handling of non-deterministic output. Also learn real background processing in .NET — deliberately with an in-process queue first, so Phase 7's broker swap has something honest to replace.

## Key concepts to learn

- Ollama: pulling/serving models, the OpenAI-compatible API, GPU/CPU tradeoffs, quantization basics
- `Microsoft.Extensions.AI` (`IChatClient`) as the provider-agnostic seam
- Structured output: constraining the model to a JSON schema; retry/repair strategies
- OCR with Tesseract; OCR text quality vs prompt robustness
- `System.Threading.Channels` + `BackgroundService` for in-process job queues
- Driving a state machine (`FileState`) end-to-end and surfacing it in the UI

## Pipeline

`Upload (bytes → disk + FileMetadata)` → queue → `Tesseract OCR (image → raw text)` → `LLM parse (raw text → structured items)` → `Create Receipt + Items` → `FileState: ReceiptCreated`

## Tasks

- [x] **docker-compose with Ollama** — first `docker-compose.yml` at repo root; Ollama service with a volume for models. *AC: `docker compose up` + `curl localhost:11434` works.*
- [x] **Pick and document the model** — try a small instruct model with reliable JSON output (qwen3 / llama3.x class, quantized). Write a short note comparing 2 candidates on 3 sample receipts (accuracy, latency, RAM). *AC: decision note committed in obsidian.*
- [x] **Real OCR behind `IOcrService`** — Tesseract (e.g. TesseractOCR nuget or a sidecar container) replaces `DummyOCRService`. *AC: a photographed receipt yields recognizable raw text; `DummyOCRService` deleted or kept only for tests.*
- [ ] **LLM parsing service** — `IReceiptParser` in Application, implementation in Infrastructure using `Microsoft.Extensions.AI` against Ollama. Input: raw OCR text. Output: structured items `{name, quantity, unitPrice, lineTotal}` + merchant + date, validated against a JSON schema. *AC: 3 sample receipts parse into correct structured items.*
- [ ] **Prompt as a versioned artifact** — prompt template lives in a file in the repo, not inline string. Filter rules (skip totals, VAT, deposit lines) live in the prompt.
- [x] **Handle garbage output** — schema-validation failure ⇒ one retry with error feedback, then `FileState.OcrError` + "needs manual review". *AC: feeding lorem-ipsum text ends in error state, not an exception.*
- [x] **True background processing** — `Channel<FileId>` + `BackgroundService` worker; `UploadFileCommandHandler` enqueues and returns immediately (today it awaits OCR inline despite the "background" comment). *AC: upload endpoint responds < 500ms while processing continues.*
- [x] **State machine end-to-end** — `Uploaded → OcrStarted → OcrCompleted → ReceiptCreated | OcrError`, each transition persisted. (Guarding transitions inside the entity is [[Rich Domain Model]] work.)
- [ ] **Angular: upload UI** — file picker + drag-drop, spartan/ui, upload progress. *AC: user uploads a photo and sees it accepted.*
- [ ] **Angular: processing status** — poll file state with a signal `resource()` until terminal state; show state timeline. Stretch: SSE instead of polling.
- [ ] **Angular: manual correction UI** — review parsed items before/after receipt creation: edit name/qty/price, delete wrong lines. *AC: corrected items persist via UpdateReceipt.*

## Definition of done

Photograph a real grocery receipt, upload it in the browser, watch it move through states, and end with an editable structured receipt — all services local (`docker compose up` + API + Angular), no cloud calls.

## Resources

- [Ollama docs](https://github.com/ollama/ollama/tree/main/docs)
- [Microsoft.Extensions.AI (learn.microsoft.com)](https://learn.microsoft.com/dotnet/ai/microsoft-extensions-ai)
- [Structured outputs with Ollama](https://ollama.com/blog/structured-outputs)
- [Background tasks with hosted services (learn.microsoft.com)](https://learn.microsoft.com/aspnet/core/fundamentals/host/hosted-services)
- [System.Threading.Channels intro (devblogs)](https://devblogs.microsoft.com/dotnet/an-introduction-to-system-threading-channels/)
