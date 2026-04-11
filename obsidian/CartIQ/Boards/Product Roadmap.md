---

kanban-plugin: board

---

---
kanban-plugin: board
---

## 🧠 Ideas / Backlog
- [ ] Define core value proposition (Grocery Optimizer)
- [ ] Decide DB (PostgreSQL vs SQL Server)
- [ ] Choose OCR approach (Tesseract vs API)
- [ ] Define product categories (protein, carbs, etc.)
- [ ] Define macro schema (protein, carbs, fats, kcal)
- [ ] Research :contentReference[oaicite:0]{index=0} API usage
- [ ] Define optimization strategy (simple vs knapsack)
- [ ] Design basic UI wireframes

---

## 🏗️ Milestone 1 — Foundation
- [ ] Setup ASP.NET solution (API, Application, Domain, Infrastructure)
- [ ] Setup Angular project
- [ ] Setup database + EF Core
- [ ] Create core entities:
  - [ ] User
  - [ ] Receipt
  - [ ] ReceiptItem
- [ ] Implement basic CRUD for receipts
- [ ] Setup file upload endpoint
- [ ] Create Angular layout (navbar, routing)
- [ ] Build receipt upload UI

---

## 🧾 Milestone 2 — Receipt Processing
- [ ] Integrate OCR service
- [ ] Extract raw text from receipt images
- [ ] Implement line parsing logic
- [ ] Filter irrelevant lines (totals, VAT, etc.)
- [ ] Store parsed receipt items
- [ ] Display extracted items in UI
- [ ] Build manual correction UI:
  - [ ] Edit item name
  - [ ] Delete incorrect items

---

## 🧠 Milestone 3 — Product & Nutrition System
- [ ] Create Product entity
- [ ] Create Category entity
- [ ] Create ProductMapping entity (rawName → product)
- [ ] Seed initial categories (chicken, skyr, tuna, rice, etc.)
- [ ] Implement product matching service:
  - [ ] Exact match
  - [ ] Fallback to category
- [ ] Add manual mapping endpoint
- [ ] Build UI for assigning product/category to items
- [ ] Attach macros to products
- [ ] Display macros per item + per receipt

---

## 📊 Milestone 4 — Tracking & Insights
- [ ] Implement spend aggregation (daily/weekly)
- [ ] Calculate cost per category
- [ ] Calculate protein per €
- [ ] Identify top purchased products
- [ ] Identify most expensive items
- [ ] Build dashboard UI:
  - [ ] Spending chart
  - [ ] Macro overview
  - [ ] Category breakdown
- [ ] Build insights panel:
  - [ ] “Top protein sources”
  - [ ] “High cost items”

---

## ⚙️ Milestone 5 — Optimization Engine
- [ ] Create optimization service
- [ ] Implement basic algorithm:
  - [ ] Rank products by protein/€
  - [ ] Generate shopping list within budget
- [ ] Build input form:
  - [ ] Budget
  - [ ] Target calories
  - [ ] Target protein
- [ ] Generate optimized shopping list
- [ ] Display total cost + macros
- [ ] Add simple suggestions:
  - [ ] “Replace X with Y”
  - [ ] “You overspend on category Z”

---

## 🚀 Polish & Enhancements
- [ ] Improve OCR accuracy
- [ ] Add fuzzy matching (string similarity)
- [ ] Integrate :contentReference[oaicite:1]{index=1} for product lookup
- [ ] Add barcode scanning support
- [ ] Improve UI/UX design
- [ ] Add authentication (JWT)
- [ ] Add error handling & logging
- [ ] Optimize DB queries

---

## ✅ Done





%% kanban:settings
```
{"kanban-plugin":"board","list-collapse":[false]}
```
%%