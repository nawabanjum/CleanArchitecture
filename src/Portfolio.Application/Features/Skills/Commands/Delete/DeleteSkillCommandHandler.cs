using MediatR;
using Portfolio.Application.Common.Exceptions;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Skills.Commands.Delete;

public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public DeleteSkillCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.Portfolios.GetByIdAsync(request.PortfolioId)
            ?? throw new NotFoundException(nameof(Portfolio), request.PortfolioId);

        if (portfolio.UserId != _currentUserService.UserId)
            throw new ForbiddenException();

        var skill = await _unitOfWork.Skills.GetByIdAsync(request.Id)
            ?? throw new NotFoundException("Skill", request.Id);

        _unitOfWork.Skills.Delete(skill);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
