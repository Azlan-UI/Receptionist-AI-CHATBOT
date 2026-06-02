using System.Net.Http.Json;
using ReceptionistChatBot.Application.DTOs.Chat;

namespace ReceptionistChatBot.Web.Services;

public sealed class ChatApiClient
{
    private readonly HttpClient _httpClient;

    public ChatApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Guid> CreateSessionAsync(CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.PostAsync("/api/chat/session", content: null, cancellationToken);
        await EnsureSuccessAsync(response, cancellationToken);

        var body = await response.Content.ReadFromJsonAsync<CreateChatSessionResponse>(cancellationToken);

        return body?.SessionId
            ?? throw new InvalidOperationException("The API did not return a chat session id.");
    }

    public async Task<ChatResponseDto> SendMessageAsync(
        Guid sessionId,
        string message,
        CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.PostAsJsonAsync(
            "/api/chat/send",
            new SendChatMessageRequest(sessionId, message),
            cancellationToken);

        await EnsureSuccessAsync(response, cancellationToken);

        return await response.Content.ReadFromJsonAsync<ChatResponseDto>(cancellationToken)
            ?? throw new InvalidOperationException("The API did not return a chat response.");
    }

    public async Task<IReadOnlyList<ChatMessageDto>> GetHistoryAsync(
        Guid sessionId,
        CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<IReadOnlyList<ChatMessageDto>>(
            $"/api/chat/history/{sessionId}",
            cancellationToken)
            ?? [];
    }

    public async Task<IReadOnlyList<ChatSessionDto>> GetSessionsAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<IReadOnlyList<ChatSessionDto>>(
            "/api/chat/sessions",
            cancellationToken)
            ?? [];
    }

    private static async Task EnsureSuccessAsync(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var error = await response.Content.ReadAsStringAsync(cancellationToken);

        throw new HttpRequestException(
            $"Chat API request failed with status {(int)response.StatusCode}. {error}");
    }

    private sealed record CreateChatSessionResponse(Guid SessionId);

    private sealed record SendChatMessageRequest(Guid? SessionId, string Message);
}
