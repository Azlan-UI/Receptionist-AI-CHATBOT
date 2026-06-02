using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ReceptionistChatBot.Application.Interfaces.Services;

namespace ReceptionistChatBot.Infrastructure.AI;

public sealed class GeminiService : IGeminiService
{
    private readonly HttpClient _httpClient;
    private readonly GeminiOptions _options;
    private readonly ILogger<GeminiService> _logger;

    public GeminiService(
        HttpClient httpClient,
        IOptions<GeminiOptions> options,
        ILogger<GeminiService> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<string> GenerateResponseAsync(
        string prompt,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(prompt))
        {
            throw new ArgumentException("Prompt cannot be empty.", nameof(prompt));
        }

        if (string.IsNullOrWhiteSpace(_options.ApiKey))
        {
            _logger.LogError("Gemini API key is missing from configuration.");
            throw new InvalidOperationException("Gemini API key is not configured.");
        }

        var request = new GeminiGenerateContentRequest(
            [
                new GeminiContent(
                    "user",
                    [new GeminiPart(prompt)])
            ]);

        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Post,
            $"/v1beta/models/{Uri.EscapeDataString(_options.Model)}:generateContent")
        {
            Content = JsonContent.Create(request)
        };

        httpRequest.Headers.Add("x-goog-api-key", _options.ApiKey);

        try
        {
            using var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                await LogGeminiErrorAsync(response, cancellationToken);
                throw new HttpRequestException(
                    "Gemini API request failed.",
                    null,
                    response.StatusCode);
            }

            var geminiResponse = await response.Content.ReadFromJsonAsync<GeminiGenerateContentResponse>(
                cancellationToken: cancellationToken);

            var generatedText = geminiResponse?
                .Candidates?
                .SelectMany(candidate => candidate.Content?.Parts ?? [])
                .Select(part => part.Text)
                .FirstOrDefault(text => !string.IsNullOrWhiteSpace(text));

            if (string.IsNullOrWhiteSpace(generatedText))
            {
                _logger.LogWarning("Gemini API returned a successful response without generated text.");
                return string.Empty;
            }

            return generatedText;
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            _logger.LogError("Gemini API request timed out.");
            throw;
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Gemini API request failed.");
            throw;
        }
    }

    private async Task LogGeminiErrorAsync(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        var responseBody = response.StatusCode == HttpStatusCode.NoContent
            ? string.Empty
            : await response.Content.ReadAsStringAsync(cancellationToken);

        _logger.LogError(
            "Gemini API returned {StatusCode}. Response: {ResponseBody}",
            (int)response.StatusCode,
            responseBody);
    }

    private sealed record GeminiGenerateContentRequest(
        [property: JsonPropertyName("contents")] IReadOnlyList<GeminiContent> Contents);

    private sealed record GeminiContent(
        [property: JsonPropertyName("role")] string Role,
        [property: JsonPropertyName("parts")] IReadOnlyList<GeminiPart> Parts);

    private sealed record GeminiPart(
        [property: JsonPropertyName("text")] string Text);

    private sealed record GeminiGenerateContentResponse(
        [property: JsonPropertyName("candidates")] IReadOnlyList<GeminiCandidate>? Candidates);

    private sealed record GeminiCandidate(
        [property: JsonPropertyName("content")] GeminiContentResponse? Content);

    private sealed record GeminiContentResponse(
        [property: JsonPropertyName("parts")] IReadOnlyList<GeminiPartResponse>? Parts);

    private sealed record GeminiPartResponse(
        [property: JsonPropertyName("text")] string? Text);
}
