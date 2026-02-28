using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Features.Experiences.Commands.Create;
using Portfolio.Application.Features.Experiences.Commands.Delete;
using Portfolio.Application.Features.Experiences.Commands.Update;

namespace Portfolio.API.Controllers;

[ApiController]
[Authorize]
[Route("api/portfolios/{portfolioId:guid}/experiences")]
public class ExperiencesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExperiencesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid portfolioId, [FromBody] CreateExperienceCommand command)
    {
        command.PortfolioId = portfolioId;
        var result = await _mediator.Send(command);
        return Created($"api/portfolios/{portfolioId}/experiences/{result.Id}", result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid portfolioId, Guid id, [FromBody] UpdateExperienceCommand command)
    {
        command.Id = id;
        command.PortfolioId = portfolioId;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid portfolioId, Guid id)
    {
        await _mediator.Send(new DeleteExperienceCommand { Id = id, PortfolioId = portfolioId });
        return NoContent();
    }
}
