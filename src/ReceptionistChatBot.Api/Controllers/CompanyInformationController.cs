using Microsoft.AspNetCore.Mvc;
using ReceptionistChatBot.Application.DTOs.Admin;
using ReceptionistChatBot.Application.Interfaces.Services;

namespace ReceptionistChatBot.Api.Controllers;

[ApiController]
[Route("api/company-information")]
public sealed class CompanyInformationController : ControllerBase
{
    private readonly ICompanyInformationService _companyInformationService;

    public CompanyInformationController(ICompanyInformationService companyInformationService)
    {
        _companyInformationService = companyInformationService;
    }

    [HttpGet("active")]
    [ProducesResponseType(typeof(CompanyInformationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompanyInformationDto>> GetActiveAsync(CancellationToken cancellationToken)
    {
        var company = await _companyInformationService.GetActiveAsync(cancellationToken);
        if (company is null)
        {
            return NotFound();
        }

        return Ok(company);
    }

    [HttpPut]
    [ProducesResponseType(typeof(CompanyInformationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CompanyInformationDto>> UpsertAsync([FromBody] UpdateCompanyInformationRequest request, CancellationToken cancellationToken)
    {
        var company = await _companyInformationService.UpsertAsync(request, cancellationToken);
        return Ok(company);
    }
}
