using Microsoft.EntityFrameworkCore;
using ReceptionistChatBot.Application.DTOs.Admin;
using ReceptionistChatBot.Application.Interfaces.Repositories;
using ReceptionistChatBot.Application.Interfaces.Services;
using ReceptionistChatBot.Domain.Entities;
using ReceptionistChatBot.Infrastructure.Persistence;

namespace ReceptionistChatBot.Infrastructure.Services;

public sealed class CompanyInformationService : ICompanyInformationService
{
    private readonly ReceptionistChatBotDbContext _dbContext;

    public CompanyInformationService(ReceptionistChatBotDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CompanyInformationDto?> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        var company = await _dbContext.CompanyInformation
            .AsNoTracking()
            .OrderByDescending(c => c.UpdatedAtUtc ?? c.CreatedAtUtc)
            .FirstOrDefaultAsync(c => c.IsActive, cancellationToken);

        return company is null ? null : MapToDto(company);
    }

    public async Task<CompanyInformationDto> UpsertAsync(UpdateCompanyInformationRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var existing = await _dbContext.CompanyInformation
            .OrderByDescending(c => c.UpdatedAtUtc ?? c.CreatedAtUtc)
            .FirstOrDefaultAsync(cancellationToken);

        if (existing is not null)
        {
            existing.CompanyName = request.CompanyName.Trim();
            existing.Description = request.Description?.Trim();
            existing.Address = request.Address?.Trim();
            existing.PhoneNumber = request.PhoneNumber?.Trim();
            existing.Email = request.Email?.Trim();
            existing.WebsiteUrl = request.WebsiteUrl?.Trim();
            existing.BusinessHours = request.BusinessHours?.Trim();
            existing.IsActive = request.IsActive;
        }
        else
        {
            existing = new CompanyInformation
            {
                Id = Guid.NewGuid(),
                CompanyName = request.CompanyName.Trim(),
                Description = request.Description?.Trim(),
                Address = request.Address?.Trim(),
                PhoneNumber = request.PhoneNumber?.Trim(),
                Email = request.Email?.Trim(),
                WebsiteUrl = request.WebsiteUrl?.Trim(),
                BusinessHours = request.BusinessHours?.Trim(),
                IsActive = request.IsActive
            };

            _dbContext.CompanyInformation.Add(existing);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return MapToDto(existing);
    }

    private static CompanyInformationDto MapToDto(CompanyInformation company) => new(
        company.Id,
        company.CompanyName,
        company.Description,
        company.Address,
        company.PhoneNumber,
        company.Email,
        company.WebsiteUrl,
        company.BusinessHours,
        company.IsActive);
}
