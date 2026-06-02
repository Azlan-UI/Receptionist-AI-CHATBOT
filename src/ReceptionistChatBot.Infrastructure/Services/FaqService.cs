using ReceptionistChatBot.Application.Common.Exceptions;
using ReceptionistChatBot.Application.DTOs.Admin;
using ReceptionistChatBot.Application.Interfaces.Repositories;
using ReceptionistChatBot.Application.Interfaces.Services;
using ReceptionistChatBot.Domain.Entities;

namespace ReceptionistChatBot.Infrastructure.Services;

public sealed class FaqService : IFaqService
{
    private readonly IFaqRepository _faqRepository;
    private readonly IUnitOfWork _unitOfWork;

    public FaqService(IFaqRepository faqRepository, IUnitOfWork unitOfWork)
    {
        _faqRepository = faqRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<FaqDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var faqs = await _faqRepository.ListAsync(cancellationToken);
        return faqs.Select(MapToDto).ToList();
    }

    public async Task<FaqDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var faq = await _faqRepository.GetByIdAsync(id, cancellationToken);
        return faq is null ? null : MapToDto(faq);
    }

    public async Task<FaqDto> CreateAsync(CreateFaqRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var faq = new Faq
        {
            Id = Guid.NewGuid(),
            Question = request.Question.Trim(),
            Answer = request.Answer.Trim(),
            Category = request.Category.Trim(),
            Keywords = request.Keywords?.Trim(),
            DisplayOrder = request.DisplayOrder,
            IsActive = request.IsActive
        };

        await _faqRepository.AddAsync(faq, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(faq);
    }

    public async Task<FaqDto?> UpdateAsync(Guid id, UpdateFaqRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var faq = await _faqRepository.GetByIdAsync(id, cancellationToken);
        if (faq is null)
        {
            throw new NotFoundException($"FAQ '{id}' was not found.");
        }

        faq.Question = request.Question.Trim();
        faq.Answer = request.Answer.Trim();
        faq.Category = request.Category.Trim();
        faq.Keywords = request.Keywords?.Trim();
        faq.DisplayOrder = request.DisplayOrder;
        faq.IsActive = request.IsActive;

        _faqRepository.Update(faq);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(faq);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var faq = await _faqRepository.GetByIdAsync(id, cancellationToken);
        if (faq is null)
        {
            throw new NotFoundException($"FAQ '{id}' was not found.");
        }

        _faqRepository.Remove(faq);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static FaqDto MapToDto(Faq faq) => new(
        faq.Id,
        faq.Question,
        faq.Answer,
        faq.Category,
        faq.Keywords,
        faq.DisplayOrder,
        faq.IsActive);
}
