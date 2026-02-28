using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Features.Certifications.Commands.Create;
using Portfolio.Application.Features.Certifications.Commands.Delete;
using Portfolio.Application.Features.Certifications.Commands.Update;

namespace Portfolio.API.Controllers;

[ApiController]
[Authorize]
[Route("api/portfolios/{portfolioId:guid}/certifications")]
public class CertificationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CertificationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid portfolioId, [FromBody] CreateCertificationCommand command)
    {
        command.PortfolioId = portfolioId;
        var result = await _mediator.Send(command);
        return Created($"api/portfolios/{portfolioId}/certifications/{result.Id}", result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid portfolioId, Guid id, [FromBody] UpdateCertificationCommand command)
    {
        command.Id = id;
        command.PortfolioId = portfolioId;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid portfolioId, Guid id)
    {
        await _mediator.Send(new DeleteCertificationCommand { Id = id, PortfolioId = portfolioId });
        return NoContent();
    }
}
