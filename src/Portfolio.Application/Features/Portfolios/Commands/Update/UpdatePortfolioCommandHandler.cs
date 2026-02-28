using AutoMapper;
using MediatR;
using Portfolio.Application.Common.Exceptions;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Portfolios.Commands.Update;

public class UpdatePortfolioCommandHandler : IRequestHandler<UpdatePortfolioCommand, PortfolioDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public UpdatePortfolioCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<PortfolioDto> Handle(UpdatePortfolioCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.Portfolios.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Portfolio), request.Id);

        if (portfolio.UserId != _currentUserService.UserId)
            throw new ForbiddenException();

        _mapper.Map(request, portfolio);
        portfolio.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Portfolios.Update(portfolio);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<PortfolioDto>(portfolio);
    }
}
