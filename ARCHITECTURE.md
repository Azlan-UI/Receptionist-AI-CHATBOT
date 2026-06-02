# AI Receptionist Chatbot Architecture

## Folder Structure

```text
ReceptionistChatBot/
  src/
    ReceptionistChatBot.Api/
      Controllers/
    ReceptionistChatBot.Web/
      Components/
    ReceptionistChatBot.Application/
      DTOs/
        Appointments/
        Chat/
        KnowledgeBase/
        Patients/
        Staff/
      Interfaces/
        Repositories/
        Services/
    ReceptionistChatBot.Domain/
      Common/
      Entities/
      Enums/
    ReceptionistChatBot.Infrastructure/
      Persistence/
      Repositories/
```

## Project Structure

```text
ReceptionistChatBot.Api            -> ASP.NET Core Web API host
ReceptionistChatBot.Web            -> Blazor Server frontend
ReceptionistChatBot.Application    -> DTOs, repository interfaces, service interfaces
ReceptionistChatBot.Domain         -> Domain models and domain enums
ReceptionistChatBot.Infrastructure -> EF Core, PostgreSQL, repository implementations later
```

## Dependency Direction

```text
Api  -> Application, Infrastructure
Web  -> Application
Infrastructure -> Application, Domain
Application -> Domain
Domain -> No project dependencies
```

## Current Scope

This skeleton intentionally includes only:

- Domain models
- DTOs
- Repository interfaces
- Service interfaces
- Project and folder structure

Implementation code, controllers, DbContext, EF Core mappings, dependency injection registration, and Blazor components are intentionally left for the next phase.
