using MediatR;
using Portfolio.Application.Common.Exceptions;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Projects.Commands.Delete;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public DeleteProjectCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.Portfolios.GetByIdAsync(request.PortfolioId)
            ?? throw new NotFoundException(nameof(Portfolio), request.PortfolioId);

        if (portfolio.UserId != _currentUserService.UserId)
            throw new ForbiddenException();

        var project = await _unitOfWork.Projects.GetByIdAsync(request.Id)
            ?? throw new NotFoundException("Project", request.Id);

        _unitOfWork.Projects.Delete(project);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
