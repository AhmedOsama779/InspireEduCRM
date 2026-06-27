# InspireEduCRM

A CRM system for Inspire Edu's sales workflow — tracking schools, contacts, sales visits, the lead pipeline, and customer follow-ups.

Built for the **Senior Full Stack Developer Practical Project Assessment** (Phase 1 — mandatory scope only). The original spec called for Laravel + Next.js; this submission uses **.NET 9 + Angular** instead, with MySQL kept as specified. See `docs/architecture.docx` for the full reasoning behind this and other design decisions.

## Tech Stack

| Layer | Technology |
|---|---|
| Backend | .NET 9, ASP.NET Core Web API, Entity Framework Core 9 |
| Frontend | Angular 20 (standalone components), Bootstrap 5 |
| Database | MySQL (via Pomelo.EntityFrameworkCore.MySql) |
| Auth | JWT Bearer tokens, BCrypt password hashing |
| API Docs | Swagger / OpenAPI (Swashbuckle) |

## Project Structure

```
InspireEduCRM/
├── InspireEduCRM.API/             # Controllers, Program.cs, Swagger, JWT config
├── InspireEduCRM.Application/     # Services (business logic), DTOs
├── InspireEduCRM.Domain/          # Entities, Enums (no dependencies)
├── InspireEduCRM.Infrastructure/  # EF Core DbContext, MySQL configuration
├── InspireEduCRM-Frontend/        # Angular application
├── InspireEduCRM.sln
└── docs/
    ├── architecture.docx
    └── erd.png
```

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org) (v20+) and npm
- [Angular CLI](https://angular.dev) (`npm install -g @angular/cli`)
- MySQL Server (8.x recommended) running locally
- Visual Studio 2022 (or any IDE with .NET 9 support)

## Backend Setup

1. **Clone the repo and open the solution**
   ```
   git clone https://github.com/AhmedOsama779/InspireEduCRM.git
   cd InspireEduCRM
   ```
   Open `InspireEduCRM.sln` in Visual Studio.

2. **Create the database**

   In MySQL, run:
   ```sql
   CREATE DATABASE InspireEduCrmDb;
   ```

3. **Configure the connection string**

   Open `InspireEduCRM.API/appsettings.json` and set your own local MySQL password:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Port=3306;Database=InspireEduCrmDb;User=root;Password=YOUR_MYSQL_PASSWORD_HERE;"
   }
   ```

4. **Apply migrations**

   In Visual Studio's Package Manager Console (default project: `InspireEduCRM.Infrastructure`):
   ```
   Update-Database -StartupProject InspireEduCRM.API
   ```
   This creates all tables (Users, Schools, Contacts, Books, Visits, VisitBooks, Leads, FollowUps).

5. **Create an initial Admin user**

   There is no public registration endpoint by design (only an Admin should be able to create accounts in a real deployment). To create the first user, run the API once, use the `/api/Auth` Swagger page to generate a BCrypt hash for your chosen password (see note below), then insert directly via MySQL:
   ```sql
   INSERT INTO Users (Name, Email, PasswordHash, Role)
   VALUES ('Admin User', 'admin@inspireedu.com', '<bcrypt-hash>', 0);
   ```
   `Role` is an integer: `0 = Admin`, `1 = SalesRepresentative`, `2 = CustomerService`.

6. **Run the API**

   Press F5 in Visual Studio, or:
   ```
   dotnet run --project InspireEduCRM.API
   ```
   Swagger UI will be available at `https://localhost:7198/swagger` (port may vary — check the console output).

   First run only: trust the local HTTPS dev certificate if prompted:
   ```
   dotnet dev-certs https --trust
   ```

## Frontend Setup

1. **Install dependencies**
   ```
   cd InspireEduCRM-Frontend
   npm install
   ```

2. **Check the API URL**

   Each service file under `src/app/services/` points to `https://localhost:7198/api/...`. If your backend runs on a different port, update the `apiUrl` constant in each service file to match.

3. **Run the app**
   ```
   ng serve
   ```
   Open `http://localhost:4200` in your browser. You'll be redirected to `/login`.

## Running the Full App

Both projects must be running simultaneously:
1. Start the backend (Visual Studio, F5)
2. Start the frontend (`ng serve` from `InspireEduCRM-Frontend/`)
3. Log in at `http://localhost:4200/login` with the Admin account created in setup step 5

## Core Workflow (Phase 1)

1. **Admin / Sales Rep** creates a **School**, adds **Contacts** at that school
2. **Sales Rep** logs a **Visit** (books presented, interest level) — this automatically creates a **Lead** for the school if one doesn't exist
3. **Customer Service** logs **Follow-Ups** (calls, meetings, notes) against the Lead, and advances its pipeline stage: `Lead → Qualified → Interested → FollowUp → Won/Lost`
4. The School's detail page in the frontend shows all of this in one view: info, Lead stage, Contacts, Visits, and Follow-Ups

## Role-Based Access

| Action | Admin | Sales Representative | Customer Service |
|---|---|---|---|
| Manage Schools / Contacts | ✅ | ✅ | ✅ |
| Log a Visit | ✅ | ✅ | ❌ |
| Update Lead stage | ✅ | ✅ | ✅ |
| Log a Follow-Up | ✅ | ❌ | ✅ |

## API Documentation

Full interactive API documentation is available via Swagger once the backend is running, at `/swagger`. Every endpoint requires a Bearer token except `POST /api/Auth/login` — use the **Authorize** button in Swagger and paste `Bearer <your-token>` after logging in.

## Known Simplifications

These are deliberate, scoped-down choices made to fit the Phase 1 timeline — see `docs/architecture.docx` for the full reasoning:

- **JWT signing key** is stored in `appsettings.json` for local development convenience. In a real deployment this would live in a secrets manager and never be committed to source control.
- **One Lead per School**: a school can only have a single Lead at a time (enforced at the database level). Re-engaging a previously Lost lead is a known future extension.
- **Visits and Follow-Ups are Create-only** (no edit), treated as historical event logs rather than editable records, consistent with the assessment's wording.
- **JWT tokens are stored in browser `localStorage`** on the frontend — standard for an internal tool, though a production-grade app facing external users would consider httpOnly cookies instead.

## Database Design

See `docs/erd.png` for the full entity-relationship diagram, and `docs/architecture.docx` for the reasoning behind key modeling decisions (e.g. why Books is a real entity with a many-to-many join table, why Lead is separate from School).
