using Microsoft.AspNetCore.Mvc;
using ReceptionistChatBot.Application.DTOs.Admin;
using ReceptionistChatBot.Application.Interfaces.Services;

namespace ReceptionistChatBot.Api.Controllers;

[ApiController]
[Route("api/faqs")]
public sealed class FaqController : ControllerBase
{
    private readonly IFaqService _faqService;

    public FaqController(IFaqService faqService)
    {
        _faqService = faqService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<FaqDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<FaqDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var faqs = await _faqService.GetAllAsync(cancellationToken);
        return Ok(faqs);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(FaqDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FaqDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var faq = await _faqService.GetByIdAsync(id, cancellationToken);
        if (faq is null)
        {
            return NotFound();
        }

        return Ok(faq);
    }

    [HttpPost]
    [ProducesResponseType(typeof(FaqDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FaqDto>> CreateAsync([FromBody] CreateFaqRequest request, CancellationToken cancellationToken)
    {
        var faq = await _faqService.CreateAsync(request, cancellationToken);
        return Created($"/api/faqs/{faq.Id}", faq);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(FaqDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FaqDto>> UpdateAsync(Guid id, [FromBody] UpdateFaqRequest request, CancellationToken cancellationToken)
    {
        var faq = await _faqService.UpdateAsync(id, request, cancellationToken);
        return Ok(faq);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _faqService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
