# Chat API Endpoints

## POST /api/chat/send

Sends a user message to the AI receptionist. If `sessionId` is omitted, a new chat session is created automatically.

Request:

```json
{
  "sessionId": "00000000-0000-0000-0000-000000000000",
  "visitorName": "Jane Doe",
  "visitorContact": "jane@example.com",
  "message": "What are your business hours?"
}
```

Responses:

- `200 OK` with `ChatResponseDto`
- `400 Bad Request` for validation errors
- `404 Not Found` when the supplied session does not exist
- `502 Bad Gateway` when the AI provider call fails

## GET /api/chat/history/{sessionId}

Returns the message history for a chat session.

Responses:

- `200 OK` with `IReadOnlyList<ChatMessageDto>`
- `404 Not Found` when the session does not exist

## POST /api/chat/session

Creates a new chat session.

Responses:

- `201 Created` with `{ "sessionId": "..." }`

## GET /api/chat/sessions

Returns chat sessions ordered by most recent creation time.

Responses:

- `200 OK` with `IReadOnlyList<ChatSessionDto>`

## Error Handling

The API uses global exception handling and returns `application/problem+json` responses for validation, not found, external AI provider, cancellation, and unexpected errors.
