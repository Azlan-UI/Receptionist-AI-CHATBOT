using System.Net.Http.Json;
using ReceptionistChatBot.Application.DTOs.Admin;

namespace ReceptionistChatBot.Web.Services;

public sealed class CompanyInformationApiClient
{
    private readonly HttpClient _httpClient;

    public CompanyInformationApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CompanyInformationDto?> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<CompanyInformationDto>("/api/company-information/active", cancellationToken);
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }

    public async Task<CompanyInformationDto> UpsertAsync(UpdateCompanyInformationRequest request, CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.PutAsJsonAsync("/api/company-information", request, cancellationToken);
        await EnsureSuccessAsync(response, cancellationToken);

        return await response.Content.ReadFromJsonAsync<CompanyInformationDto>(cancellationToken)
            ?? throw new InvalidOperationException("The API did not return company information.");
    }

    private static async Task EnsureSuccessAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var error = await response.Content.ReadAsStringAsync(cancellationToken);
        throw new HttpRequestException($"Company information API request failed with status {(int)response.StatusCode}. {error}");
    }
}
