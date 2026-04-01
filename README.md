# Mission 12 — Bookstore (categories, cart, Bootstrap)

Extends Mission 11 with **category filtering** (pagination reflects the filtered set), a **session cart** (quantity, line subtotals, order total) with **Continue shopping** returning to the same list URL you had when you last added a book, and a **cart summary** on the catalog page. Layout uses the **Bootstrap grid** (`container-fluid`, `row`, `col-*`).

## Run locally

**1. API** (from `Mission12/BookstoreAPI`):

```bash
dotnet run
```

API: http://localhost:5103

**2. React app** (from `Mission12/bookstore-app`):

```bash
npm install
npm run dev
```

App: http://localhost:5173

---

## Learning Suite comment — Bootstrap (#notcoveredinthevideos)

Paste something like this with your public GitHub link:

1. **Offcanvas** — In `bookstore-app/src/components/Layout.tsx`: sliding cart preview panel using classes `offcanvas`, `offcanvas-end`, `offcanvas-header`, `offcanvas-body`, `offcanvas-title`, plus the navbar button with `data-bs-toggle="offcanvas"` and `data-bs-target="#cartOffcanvas"`. Bootstrap’s JS bundle is imported in `src/main.tsx` so the panel opens and closes.

2. **Accordion** — In `bookstore-app/src/components/BookList.tsx`: sidebar help panel using `accordion`, `accordion-flush`, `accordion-item`, `accordion-header`, `accordion-button`, `accordion-collapse`, `accordion-body`, `data-bs-toggle="collapse"`, `data-bs-target`, and `data-bs-parent="#browseTips"`.
