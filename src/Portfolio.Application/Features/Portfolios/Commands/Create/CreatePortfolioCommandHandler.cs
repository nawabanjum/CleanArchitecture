using AutoMapper;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Portfolios.Commands.Create;

public class CreatePortfolioCommandHandler : IRequestHandler<CreatePortfolioCommand, PortfolioDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public CreatePortfolioCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<PortfolioDto> Handle(CreatePortfolioCommand request, CancellationToken cancellationToken)
    {
        var portfolio = _mapper.Map<Domain.Entities.Portfolio>(request);
        portfolio.UserId = _currentUserService.UserId!;

        await _unitOfWork.Portfolios.AddAsync(portfolio);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<PortfolioDto>(portfolio);
    }
}
