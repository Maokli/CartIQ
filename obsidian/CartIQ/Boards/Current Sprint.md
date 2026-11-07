---

kanban-plugin: board

---

## 📥 To Do

- [ ] LLM parsing service: OCR text → structured items via Microsoft.Extensions.AI [[Local LLM Receipt Parsing]]
- [ ] Prompt as versioned file (incl. filter rules: totals, VAT, deposit lines) [[Local LLM Receipt Parsing]]
- [ ] Store parsed receipt + items from LLM output [[Local LLM Receipt Parsing]]
- [ ] Handle garbage LLM output: retry once, then OcrError + manual review [[Local LLM Receipt Parsing]]
- [ ] Angular: upload UI (drag-drop, progress) [[Local LLM Receipt Parsing]]
- [ ] Angular: processing status via polling resource() (stretch: SSE) [[Local LLM Receipt Parsing]]
- [ ] Angular: manual correction UI (edit names, delete wrong items) [[Local LLM Receipt Parsing]]


## 🚧 In Progress



## ✅ Done

- [ ] Real OCR: Tesseract behind IOcrService, delete DummyOCRService [[Local LLM Receipt Parsing]]
- [ ] Drive FileState machine end-to-end, persisted per transition [[Local LLM Receipt Parsing]]
- [ ] True background processing: Channel + BackgroundService [[Local LLM Receipt Parsing]]
- [ ] docker-compose with Ollama (start of the local stack) [[Local LLM Receipt Parsing]]
- [ ] Pick local model + write decision note (accuracy/latency/RAM on 3 receipts) [[Local LLM Receipt Parsing]]




%% kanban:settings
```
{"kanban-plugin":"board","list-collapse":[false,false,false]}
```
%%