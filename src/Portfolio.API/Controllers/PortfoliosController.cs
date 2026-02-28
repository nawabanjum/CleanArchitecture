using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Features.Portfolios.Commands.Create;
using Portfolio.Application.Features.Portfolios.Commands.Delete;
using Portfolio.Application.Features.Portfolios.Commands.Update;
using Portfolio.Application.Features.Portfolios.Queries.GetById;
using Portfolio.Application.Features.Portfolios.Queries.GetBySlug;
using Portfolio.Application.Features.Portfolios.Queries.GetMyPortfolios;

namespace Portfolio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PortfoliosController : ControllerBase
{
    private readonly IMediator _mediator;

    public PortfoliosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetMyPortfolios()
    {
        var result = await _mediator.Send(new GetMyPortfoliosQuery());
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePortfolioCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetPortfolioByIdQuery { Id = id });
        return Ok(result);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePortfolioCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeletePortfolioCommand { Id = id });
        return NoContent();
    }

    [HttpGet("share/{slug}")]
    public async Task<IActionResult> GetBySlug(string slug)
    {
        var result = await _mediator.Send(new GetPortfolioBySlugQuery { Slug = slug });
        return Ok(result);
    }
}
