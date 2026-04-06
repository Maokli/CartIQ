
# 🧭 Project Roadmap

We’ll break it into **5 milestones**:

1. **Foundation & Core Setup**
    
2. **Receipt → Items Pipeline**
    
3. **Product & Nutrition System**
    
4. **Insights & Tracking**
    
5. **Optimization Engine (your differentiator)**
    

---

# 🧱 Milestone 1 — Foundation & Setup (Week 1)

## 🎯 Goal:

Have a working full-stack skeleton with authentication + basic data flow.

## Backend (ASP.NET)

-  Create solution:
    
    - API project
        
    - Application layer
        
    - Domain layer
        
    - Infrastructure layer
        
-  Setup:
    
    - Entity Framework Core
        
    - SQL Server / PostgreSQL
        
-  Core entities:
    
    - User
        
    - Receipt
        
    - ReceiptItem
        
-  Basic CRUD endpoints:
    
    - upload receipt (metadata only for now)
        
    - get receipts
        

## Frontend (Angular)

-  Setup Angular project
    
-  Basic routing:
    
    - login
        
    - dashboard
        
-  Create UI shell:
    
    - navbar
        
    - layout
        
-  Receipt upload page (no processing yet)
    

---

# 🧾 Milestone 2 — Receipt → Items Pipeline (Week 2–3)

## 🎯 Goal:

Turn receipt images into structured items (even if messy)

## Backend

-  Integrate OCR:
    
    - start simple (e.g., Tesseract or external API)
        
-  Receipt processing service:
    
    - extract raw text
        
    - split into lines
        
-  Build parsing logic:
    
    - filter irrelevant lines
        
    - detect product lines
        

Example output:

```json
[
  "HÄHNCHENBRUST FILET",
  "SKYR NATUR",
  "REIS"
]
```

-  Store parsed items in DB
    

## Frontend

-  Upload receipt image
    
-  Show extracted items list
    
-  Allow manual editing:
    
    - fix names
        
    - delete wrong items
        

👉 This step is CRITICAL. Don’t over-optimize OCR.

---

# 🧠 Milestone 3 — Product & Nutrition System (Week 3–4)

## 🎯 Goal:

Attach **macros + categories** to items

## Backend

### Entities:

-  Product
    
    - name
        
    - category
        
    - protein / carbs / fats / calories
        
-  Category
    
    - chicken, dairy, carbs, etc.
        
-  ProductMapping
    
    - rawName → productId
        

---

### Logic:

-  Matching service:
    
    - exact match
        
    - fallback: category
        
-  Seed basic categories:
    
    - chicken breast
        
    - skyr
        
    - tuna
        
    - rice
        
-  Add manual mapping endpoint:
    
    - user links item → product
        

---

### Optional (nice boost):

-  Integrate Open Food Facts API
    

---

## Frontend

-  UI to:
    
    - assign category/product to item
        
-  Show macros per item
    
-  Show receipt totals:
    
    - total calories
        
    - total protein
        

---

# 📊 Milestone 4 — Tracking & Insights (Week 4–5)

## 🎯 Goal:

Turn raw data into useful insights

## Backend

-  Aggregation queries:
    
    - total spend per week
        
    - spend per category
        
    - protein per €
        
-  Insights service:
    
    - top products
        
    - most expensive categories
        

---

## Frontend

-  Dashboard:
    
    - weekly spend chart
        
    - protein vs cost
        
-  Insights UI:
    
    - “Top protein sources”
        
    - “Most expensive items”
        

---

# 🧠 Milestone 5 — Optimization Engine (Week 5–6)

## 🎯 Goal:

Build your **standout feature**

---

## Feature A — Smart Shopping List

### Input:

- budget (e.g. 40€)
    
- targets:
    
    - protein
        
    - calories
        

### Output:

- optimized list:
    
    - chicken, skyr, rice, etc.
        

---

## Backend

-  Create optimization service:
    
    - start simple:
        
        - rank products by protein/€
            
        - fill until budget reached
            
-  Later (advanced):
    
    - constraint solving (knapsack-style)
        

---

## Feature B — Suggestions

-  “Replace X with Y to save money”
    
-  “You overspend on low-protein items”
    

---

## Frontend

-  Form:
    
    - input budget + goals
        
-  Output UI:
    
    - shopping list
        
    - total cost
        
    - total macros
        

---

# 🧱 Suggested Architecture (fits your style)

Since you like structure (fnecJs vibes 👀):

## Backend Layers

- **Domain**
    
    - Entities, enums
        
- **Application**
    
    - Services (OCR, Matching, Optimization)
        
- **Infrastructure**
    
    - DB, external APIs
        
- **API**
    
    - Controllers
        

---

## Key Services

- ReceiptService
    
- OCRService
    
- ProductMatchingService
    
- NutritionService
    
- OptimizationService
    

---

# ⚠️ Scope Control (VERY IMPORTANT)

Cut these from MVP:

- ❌ perfect OCR
    
- ❌ full product database
    
- ❌ mobile app
    
- ❌ AI/ML hype features
    

Focus on:

- ✅ working pipeline
    
- ✅ usable insights
    
- ✅ one optimization feature
    