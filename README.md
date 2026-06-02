# AI Receptionist Chatbot

An intelligent chatbot system that acts as a virtual receptionist, powered by Google Gemini AI. Visitors can ask questions about company services, hours, and FAQs through a modern Blazor chat interface. Includes an admin panel for managing knowledge base content.

---

## Architecture

```
Blazor UI (Web) ──HTTP──→ .NET API ──→ PostgreSQL
                             │
                             └──→ Google Gemini AI
```

Two .NET 10 projects run simultaneously:
- **API** (`src/ReceptionistChatBot.Api`) — REST backend that handles chat logic, database access, and AI integration
- **Web** (`src/ReceptionistChatBot.Web`) — Blazor Server frontend with chat UI and admin panel

---

## Features

- **AI Chat** — Visitors can ask questions and get AI-powered responses using company data
- **Session Management** — Chat history is persisted across sessions
- **FAQ Management** — Admin can create, edit, and delete FAQs
- **Company Information** — Admin can update office hours, address, contact details
- **Human Escalation** — Chat sessions can be escalated to a human receptionist

---

## Tech Stack

| Component | Technology |
|-----------|-----------|
| Frontend | Blazor Server (.NET 10) |
| Backend API | ASP.NET Core (.NET 10) |
| Database | PostgreSQL with EF Core |
| AI | Google Gemini API (gemini-2.5-flash) |
| UI Framework | Bootstrap 5 |

---

## Prerequisites

- .NET 10 SDK
- PostgreSQL (running on localhost:5432)
- Google Gemini API key

---

## Setup

### 1. Create the database

```powershell
psql -U postgres -c "CREATE DATABASE receptionist_chatbot;"
```

### 2. Set your Gemini API key

```powershell
dotnet user-secrets set "Gemini:ApiKey" "YOUR_GEMINI_KEY" --project src\ReceptionistChatBot.Api
```

### 3. Update database connection (if needed)

Edit `src/ReceptionistChatBot.Api/appsettings.json`:

```json
"DefaultConnection": "Host=localhost;Port=5432;Database=receptionist_chatbot;Username=postgres;Password=your_password"
```

### 4. Run the API

```powershell
dotnet run --project src\ReceptionistChatBot.Api
```

API starts at `https://localhost:58157`

### 5. Run the Blazor UI (in a separate terminal)

```powershell
dotnet run --project src\ReceptionistChatBot.Web
```

UI starts at `https://localhost:58156`

---

## Usage

### Chat Interface

Open `https://localhost:58156` in your browser.

- Type a message and press Send
- The AI responds using your company's FAQs and information
- Previous sessions are listed in the sidebar

### Admin Panel

| Page | URL |
|------|-----|
| Manage FAQs | `https://localhost:58156/admin/faqs` |
| Company Info | `https://localhost:58156/admin/company` |

### API Endpoints

| Method | Path | Description |
|--------|------|-------------|
| POST | `/api/chat/send` | Send a message to the AI |
| GET | `/api/chat/history/{id}` | Get chat history |
| POST | `/api/chat/session` | Create new chat session |
| GET | `/api/chat/sessions` | List all sessions |
| GET | `/api/faqs` | List all FAQs |
| POST | `/api/faqs` | Create a FAQ |
| PUT | `/api/faqs/{id}` | Update a FAQ |
| DELETE | `/api/faqs/{id}` | Delete a FAQ |
| GET | `/api/company-information/active` | Get active company info |
| PUT | `/api/company-information` | Create or update company info |

---

## Project Structure

```
src/
├── ReceptionistChatBot.Domain/        # Entity models and enums
├── ReceptionistChatBot.Application/   # DTOs, service interfaces, repository interfaces
├── ReceptionistChatBot.Infrastructure/ # EF Core, repositories, Gemini integration
├── ReceptionistChatBot.Api/           # REST API controllers
└── ReceptionistChatBot.Web/           # Blazor Server UI
```

---

## Adding Company Data

Use the admin panel or API to add your company info and FAQs. For example via PowerShell:

```powershell
Invoke-RestMethod -Uri "https://localhost:58157/api/company-information" -Method Put -ContentType "application/json" -Body '{"companyName":"Your Company","description":"Your description","businessHours":"Mon-Fri 9AM-5PM","isActive":true}'
```
