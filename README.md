# SQL JOINs → C# API → React Dashboard

Source code for the video **[Why Your SQL JOIN Loses Users (INNER vs LEFT JOIN → C# API → React)](https://youtu.be/1piP7GzHq9U)**.

▶️ Watch it on YouTube: https://youtu.be/1piP7GzHq9U

Users without purchases can silently disappear from your app. This repo follows one small dataset through every layer and keeps every user visible:

1. **SQL Server**: `INNER JOIN` vs `LEFT JOIN`, and why `NULL` isn't an error.
2. **ASP.NET Core minimal API**: EF Core projects users into DTOs, and a user with no purchases gets an empty array (`[]`), not `null`.
3. **React**: a dashboard with loading, error, and empty states.

## The data

| Users | | Purchases | | | |
|---|---|---|---|---|---|
| **Id** | **Name** | **Id** | **UserId** | **Product** | **Amount** |
| 1 | Emma | 101 | 2 | Keyboard | 79.99 |
| 2 | James | 102 | 1 | Mouse | 24.99 |
| 3 | Sophia | 103 | 2 | Monitor | 249.99 |
| 4 | Liam | 104 | 4 | Headphones | 59.99 |
| 5 | Olivia | | | | |

Sophia and Olivia have no purchases. They are the users to watch.

## Project structure

```
sql/    SQL scripts: tables, data, INNER JOIN, LEFT JOIN, challenge solution
api/    ASP.NET Core minimal API (.NET 10, EF Core): GET /api/users/purchases
web/    React + Vite dashboard (PurchasesDashboard.jsx from the video)
```

## Run it

You need the [.NET 10 SDK](https://dotnet.microsoft.com/download) and [Node.js](https://nodejs.org/) 20 or newer.

### 1. Start the API

**Option A: SQL Server (as in the video)**

```bash
docker compose up -d          # SQL Server on localhost:1433
cd api
dotnet run                    # http://localhost:5080
```

If you already have SQL Server, change `ConnectionStrings:SqlServer` in `api/appsettings.json` and skip Docker.

**Option B: no database to install (SQLite)**

```bash
cd api
dotnet run --Database:Provider=Sqlite
```

Either way, the API creates the tables and the demo data on first run. Check it at
<http://localhost:5080/api/users/purchases>:

```json
[
  { "id": 1, "name": "Emma",   "purchases": [ { "id": 102, "product": "Mouse", "amount": 24.99 } ] },
  { "id": 2, "name": "James",  "purchases": [ { "id": 101, "product": "Keyboard", "amount": 79.99 },
                                              { "id": 103, "product": "Monitor",  "amount": 249.99 } ] },
  { "id": 3, "name": "Sophia", "purchases": [] },
  { "id": 4, "name": "Liam",   "purchases": [ { "id": 104, "product": "Headphones", "amount": 59.99 } ] },
  { "id": 5, "name": "Olivia", "purchases": [] }
]
```

### 2. Start the dashboard

```bash
cd web
npm install
npm run dev                   # http://localhost:5173
```

Vite forwards `/api` to the API on port 5080 (see `web/vite.config.js`).

### 3. Try the SQL

Run the scripts in `sql/` with SSMS, Azure Data Studio, or the VS Code `mssql` extension, in order:

| File | What it shows |
|---|---|
| `01-create-tables.sql` | `Users` and `Purchases` tables |
| `02-seed-data.sql` | The demo data |
| `03-inner-join.sql` | 4 rows: Sophia and Olivia are excluded |
| `04-left-join.sql` | 6 rows: Sophia and Olivia appear with `NULL` |
| `05-challenge-solution.sql` | ⚠ Spoiler: the challenge from the end of the video |

## Challenge

**Find every user who has no purchases, using `LEFT JOIN` and `WHERE`.**
Try it before opening `sql/05-challenge-solution.sql`.

## Notes

- Run the API and look at the log: EF Core translates the endpoint into `FROM "Users" ... LEFT JOIN "Purchases"`. Starting the query from `Users` is the C# version of the `LEFT JOIN` from the SQL part.
- The endpoint adds `.OrderBy(u => u.Id)`, which isn't shown in the video, so the response order is stable.
- The SQL Server password in `appsettings.json` and `docker-compose.yml` is for local demos only.

---

## 🇪🇸 En español

Código del video sobre **SQL JOINs → API en C# → dashboard en React**. El video está en inglés, con subtítulos en español.

- **`sql/`**: tablas, datos, `INNER JOIN` (4 filas), `LEFT JOIN` (6 filas, con `NULL` para Sophia y Olivia) y la solución del reto.
- **`api/`**: API mínima en ASP.NET Core con EF Core. Devuelve a cada usuario con su lista de compras; quien no tiene compras recibe `[]`.
- **`web/`**: dashboard en React con estados de carga, error y "No purchases yet".

**Ejecutar:**
1. **API:** `cd api` y luego `dotnet run --Database:Provider=Sqlite`. No necesitas instalar SQL Server. También puedes usar `docker compose up -d` + `dotnet run`.
2. **Dashboard:** `cd web`, `npm install` y `npm run dev`. Abre <http://localhost:5173>.

**Reto:** encuentra a los usuarios sin compras con `LEFT JOIN` y `WHERE`. La solución está en `sql/05-challenge-solution.sql`.

## License

[MIT](LICENSE)
