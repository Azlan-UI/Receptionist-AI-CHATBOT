# EF Core PostgreSQL Setup

## Infrastructure Registration

Register the infrastructure layer from the API project when implementation code is added:

```csharp
builder.Services.AddInfrastructure(
    builder.Configuration.GetConnectionString("DefaultConnection")!);
```

Recommended connection string key:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=receptionist_chatbot;Username=postgres;Password=postgres"
  }
}
```

## Migration Commands

Add a new migration:

```powershell
dotnet ef migrations add MigrationName `
  --project src\ReceptionistChatBot.Infrastructure\ReceptionistChatBot.Infrastructure.csproj `
  --startup-project src\ReceptionistChatBot.Infrastructure\ReceptionistChatBot.Infrastructure.csproj `
  --context ReceptionistChatBotDbContext `
  --output-dir Persistence\Migrations
```

Apply migrations to PostgreSQL:

```powershell
dotnet ef database update `
  --project src\ReceptionistChatBot.Infrastructure\ReceptionistChatBot.Infrastructure.csproj `
  --startup-project src\ReceptionistChatBot.Infrastructure\ReceptionistChatBot.Infrastructure.csproj `
  --context ReceptionistChatBotDbContext
```

Remove the latest migration before it is applied:

```powershell
dotnet ef migrations remove `
  --project src\ReceptionistChatBot.Infrastructure\ReceptionistChatBot.Infrastructure.csproj `
  --startup-project src\ReceptionistChatBot.Infrastructure\ReceptionistChatBot.Infrastructure.csproj `
  --context ReceptionistChatBotDbContext
```

## Notes

- `ReceptionistChatBotDbContext` lives in the Infrastructure project.
- `DesignTimeDbContextFactory` enables EF CLI commands before the API host is implemented.
- Migrations are stored in `src/ReceptionistChatBot.Infrastructure/Persistence/Migrations`.
- Repository implementations are registered through `AddInfrastructure`.
