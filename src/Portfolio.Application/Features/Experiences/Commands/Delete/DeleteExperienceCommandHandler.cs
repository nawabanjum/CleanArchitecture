using MediatR;
using Portfolio.Application.Common.Exceptions;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Experiences.Commands.Delete;

public class DeleteExperienceCommandHandler : IRequestHandler<DeleteExperienceCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public DeleteExperienceCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(DeleteExperienceCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.Portfolios.GetByIdAsync(request.PortfolioId)
            ?? throw new NotFoundException(nameof(Portfolio), request.PortfolioId);

        if (portfolio.UserId != _currentUserService.UserId)
            throw new ForbiddenException();

        var experience = await _unitOfWork.Experiences.GetByIdAsync(request.Id)
            ?? throw new NotFoundException("Experience", request.Id);

        _unitOfWork.Experiences.Delete(experience);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
