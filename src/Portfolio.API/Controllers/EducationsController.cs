using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Features.Educations.Commands.Create;
using Portfolio.Application.Features.Educations.Commands.Delete;
using Portfolio.Application.Features.Educations.Commands.Update;

namespace Portfolio.API.Controllers;

[ApiController]
[Authorize]
[Route("api/portfolios/{portfolioId:guid}/educations")]
public class EducationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EducationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid portfolioId, [FromBody] CreateEducationCommand command)
    {
        command.PortfolioId = portfolioId;
        var result = await _mediator.Send(command);
        return Created($"api/portfolios/{portfolioId}/educations/{result.Id}", result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid portfolioId, Guid id, [FromBody] UpdateEducationCommand command)
    {
        command.Id = id;
        command.PortfolioId = portfolioId;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid portfolioId, Guid id)
    {
        await _mediator.Send(new DeleteEducationCommand { Id = id, PortfolioId = portfolioId });
        return NoContent();
    }
}
