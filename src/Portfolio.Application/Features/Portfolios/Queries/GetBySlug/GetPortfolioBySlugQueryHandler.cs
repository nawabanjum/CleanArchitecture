using AutoMapper;
using MediatR;
using Portfolio.Application.Common.Exceptions;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Portfolios.Queries.GetBySlug;

public class GetPortfolioBySlugQueryHandler : IRequestHandler<GetPortfolioBySlugQuery, PortfolioDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPortfolioBySlugQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PortfolioDto> Handle(GetPortfolioBySlugQuery request, CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.Portfolios.GetBySlugAsync(request.Slug)
            ?? throw new NotFoundException(nameof(Portfolio), request.Slug);

        if (!portfolio.IsPublic)
            throw new NotFoundException(nameof(Portfolio), request.Slug);

        return _mapper.Map<PortfolioDto>(portfolio);
    }
}
