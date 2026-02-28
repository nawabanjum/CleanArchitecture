using AutoMapper;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Portfolios.Queries.GetMyPortfolios;

public class GetMyPortfoliosQueryHandler : IRequestHandler<GetMyPortfoliosQuery, List<PortfolioDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetMyPortfoliosQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<List<PortfolioDto>> Handle(GetMyPortfoliosQuery request, CancellationToken cancellationToken)
    {
        var portfolios = await _unitOfWork.Portfolios.GetByUserIdAsync(_currentUserService.UserId!);
        return _mapper.Map<List<PortfolioDto>>(portfolios);
    }
}
