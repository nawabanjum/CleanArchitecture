using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Features.SocialLinks.Commands.Create;
using Portfolio.Application.Features.SocialLinks.Commands.Delete;
using Portfolio.Application.Features.SocialLinks.Commands.Update;

namespace Portfolio.API.Controllers;

[ApiController]
[Authorize]
[Route("api/portfolios/{portfolioId:guid}/social-links")]
public class SocialLinksController : ControllerBase
{
    private readonly IMediator _mediator;

    public SocialLinksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid portfolioId, [FromBody] CreateSocialLinkCommand command)
    {
        command.PortfolioId = portfolioId;
        var result = await _mediator.Send(command);
        return Created($"api/portfolios/{portfolioId}/social-links/{result.Id}", result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid portfolioId, Guid id, [FromBody] UpdateSocialLinkCommand command)
    {
        command.Id = id;
        command.PortfolioId = portfolioId;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid portfolioId, Guid id)
    {
        await _mediator.Send(new DeleteSocialLinkCommand { Id = id, PortfolioId = portfolioId });
        return NoContent();
    }
}
