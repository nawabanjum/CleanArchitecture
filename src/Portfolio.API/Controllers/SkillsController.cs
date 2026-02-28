using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Features.Skills.Commands.Create;
using Portfolio.Application.Features.Skills.Commands.Delete;
using Portfolio.Application.Features.Skills.Commands.Update;

namespace Portfolio.API.Controllers;

[ApiController]
[Authorize]
[Route("api/portfolios/{portfolioId:guid}/skills")]
public class SkillsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SkillsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid portfolioId, [FromBody] CreateSkillCommand command)
    {
        command.PortfolioId = portfolioId;
        var result = await _mediator.Send(command);
        return Created($"api/portfolios/{portfolioId}/skills/{result.Id}", result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid portfolioId, Guid id, [FromBody] UpdateSkillCommand command)
    {
        command.Id = id;
        command.PortfolioId = portfolioId;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid portfolioId, Guid id)
    {
        await _mediator.Send(new DeleteSkillCommand { Id = id, PortfolioId = portfolioId });
        return NoContent();
    }
}
