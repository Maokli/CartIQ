# 🎛️ CartIQ Dashboard

The hub for working the roadmap. Tasks live on two boards:

- **[[Product Roadmap]]** — the full backlog, one column per phase. Cards never move here; phases get *imported* from it.
- **[[Current Sprint]]** — the working board: **📥 To Do → 🚧 In Progress → ✅ Done**. Drag cards between columns as you work.

Epic notes (learning goals, acceptance criteria, definition of done) live in `Epics/` and are linked from every card.

---

## 🚦 Phase tracker

| Phase | Epic | Status | Started | Closed |
| --- | --- | --- | --- | --- |
| 1 — Harden the Skeleton | [[Foundation Hardening]] | ✅ Closed | 2026-07-19 | 2026-11-03 |
| 2 — Receipt Pipeline + Local LLM | [[Local LLM Receipt Parsing]] | 🚧 In progress | 2026-11-03 | |
| 3 — Rich Domain + Tests | [[Rich Domain Model]] · [[Testing Pyramid]] | ⏳ Not started | | |
| 4 — Products & Open Food Facts | [[Product Catalog & Open Food Facts]] | ⏳ Not started | | |
| 5 — Modular Monolith | [[Modular Monolith]] | ⏳ Not started | | |
| 6 — Insights & Signals | [[Insights & Signals Dashboard]] | ⏳ Not started | | |
| 7 — Extract Microservices | [[Microservices Extraction]] | ⏳ Not started | | |
| 8 — Optimization Engine | — (board cards only) | ⏳ Not started | | |

Status legend: ⏳ Not started · 🚧 In progress · ✅ Closed

---

## 🎯 Active phase — exit criteria

![[Local LLM Receipt Parsing#Definition of done]]

---

## 📖 How to run a phase

### ▶️ Start a phase (import)

1. In the tracker above: set the phase to 🚧 and fill **Started**.
2. Copy every card from that phase's column on **[[Product Roadmap]]** into **📥 To Do** on **[[Current Sprint]]** (open both files in source mode, copy the `- [ ]` lines — keep the `[[epic]]` links).
3. Read the epic note top-to-bottom once — the acceptance criteria there are what "done" means per card.
4. Update the **Active phase** embed above to the new epic's `#Definition of done` section.

### 🔄 During the phase

- Drag cards **To Do → In Progress → Done**. Keep In Progress to 1–2 cards.
- New discovered work: small → add a card straight to the sprint; big → add it to the phase column on [[Product Roadmap]] instead.
- Blocked card → drag back to To Do and prefix the title with `⛔`.

### 🏁 Close a phase

1. Check the epic's **Definition of done** — every criterion must hold, not just "all cards moved".
2. On **[[Product Roadmap]]**: tick the phase's cards (`- [x]`) and move them to the **✅ Done** column.
3. Clear the ✅ Done column on **[[Current Sprint]]** (kanban right-click → Archive list, or delete the cards — the roadmap's Done column is the permanent record).
4. In the tracker: set the phase to ✅ and fill **Closed**.
5. Write a 3-line retro at the bottom of the epic note: what you learned, what surprised you, what you'd do differently.
6. Start the next phase (▶️ above).

> [!tip] Delegate the chores
> Steps like "copy all cards over" and "tick + move the phase to Done" are mechanical — you can ask Claude Code to *"start phase N"* or *"close phase N"* and it will do the board bookkeeping, update this tracker, and swap the active-phase embed for you.

---

## 🗂️ Quick links

- [[Product Roadmap]] · [[Current Sprint]]
- Epics: [[Foundation Hardening]] · [[Local LLM Receipt Parsing]] · [[Rich Domain Model]] · [[Testing Pyramid]] · [[Product Catalog & Open Food Facts]] · [[Modular Monolith]] · [[Insights & Signals Dashboard]] · [[Microservices Extraction]]
- [[Brainstorming V1]] — original product notes
