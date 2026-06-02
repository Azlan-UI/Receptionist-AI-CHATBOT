using System.Net.Http.Json;
using ReceptionistChatBot.Application.DTOs.Admin;

namespace ReceptionistChatBot.Web.Services;

public sealed class FaqApiClient
{
    private readonly HttpClient _httpClient;

    public FaqApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<FaqDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<IReadOnlyList<FaqDto>>("/api/faqs", cancellationToken)
            ?? [];
    }

    public async Task<FaqDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<FaqDto>($"/api/faqs/{id}", cancellationToken);
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }

    public async Task<FaqDto> CreateAsync(CreateFaqRequest request, CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.PostAsJsonAsync("/api/faqs", request, cancellationToken);
        await EnsureSuccessAsync(response, cancellationToken);

        return await response.Content.ReadFromJsonAsync<FaqDto>(cancellationToken)
            ?? throw new InvalidOperationException("The API did not return a FAQ.");
    }

    public async Task<FaqDto> UpdateAsync(Guid id, UpdateFaqRequest request, CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.PutAsJsonAsync($"/api/faqs/{id}", request, cancellationToken);
        await EnsureSuccessAsync(response, cancellationToken);

        return await response.Content.ReadFromJsonAsync<FaqDto>(cancellationToken)
            ?? throw new InvalidOperationException("The API did not return a FAQ.");
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.DeleteAsync($"/api/faqs/{id}", cancellationToken);
        await EnsureSuccessAsync(response, cancellationToken);
    }

    private static async Task EnsureSuccessAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var error = await response.Content.ReadAsStringAsync(cancellationToken);
        throw new HttpRequestException($"FAQ API request failed with status {(int)response.StatusCode}. {error}");
    }
}
