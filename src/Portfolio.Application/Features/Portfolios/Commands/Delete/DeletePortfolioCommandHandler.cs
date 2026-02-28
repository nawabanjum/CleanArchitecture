using MediatR;
using Portfolio.Application.Common.Exceptions;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Portfolios.Commands.Delete;

public class DeletePortfolioCommandHandler : IRequestHandler<DeletePortfolioCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public DeletePortfolioCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(DeletePortfolioCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.Portfolios.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Portfolio), request.Id);

        if (portfolio.UserId != _currentUserService.UserId)
            throw new ForbiddenException();

        _unitOfWork.Portfolios.Delete(portfolio);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
