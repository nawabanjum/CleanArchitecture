using MediatR;
using Portfolio.Application.Common.Exceptions;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Educations.Commands.Delete;

public class DeleteEducationCommandHandler : IRequestHandler<DeleteEducationCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public DeleteEducationCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.Portfolios.GetByIdAsync(request.PortfolioId)
            ?? throw new NotFoundException(nameof(Portfolio), request.PortfolioId);

        if (portfolio.UserId != _currentUserService.UserId)
            throw new ForbiddenException();

        var education = await _unitOfWork.Educations.GetByIdAsync(request.Id)
            ?? throw new NotFoundException("Education", request.Id);

        _unitOfWork.Educations.Delete(education);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
