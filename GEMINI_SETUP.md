# Gemini AI Setup

## Configuration

`GeminiService` reads settings through `IConfiguration` from the `Gemini` section:

```json
{
  "Gemini": {
    "ApiKey": "",
    "BaseUrl": "https://generativelanguage.googleapis.com",
    "Model": "gemini-2.5-flash",
    "TimeoutSeconds": 30
  }
}
```

For local development, prefer user secrets or environment variables instead of committing an API key:

```powershell
dotnet user-secrets set "Gemini:ApiKey" "YOUR_API_KEY" --project src\ReceptionistChatBot.Api\ReceptionistChatBot.Api.csproj
```

Environment variable format:

```text
Gemini__ApiKey=YOUR_API_KEY
```

## Dependency Injection

When the API host is implemented, register infrastructure with configuration:

```csharp
builder.Services.AddInfrastructure(builder.Configuration);
```

This registers:

- PostgreSQL `ReceptionistChatBotDbContext`
- Repository implementations
- `IGeminiService`
- Typed `HttpClient` for `GeminiService`

## Usage

Inject `IGeminiService` into an application service:

```csharp
var reply = await geminiService.GenerateResponseAsync(prompt, cancellationToken);
```
