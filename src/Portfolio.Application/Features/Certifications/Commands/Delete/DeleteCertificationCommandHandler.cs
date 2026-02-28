using MediatR;
using Portfolio.Application.Common.Exceptions;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Certifications.Commands.Delete;

public class DeleteCertificationCommandHandler : IRequestHandler<DeleteCertificationCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public DeleteCertificationCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(DeleteCertificationCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.Portfolios.GetByIdAsync(request.PortfolioId)
            ?? throw new NotFoundException(nameof(Portfolio), request.PortfolioId);

        if (portfolio.UserId != _currentUserService.UserId)
            throw new ForbiddenException();

        var certification = await _unitOfWork.Certifications.GetByIdAsync(request.Id)
            ?? throw new NotFoundException("Certification", request.Id);

        _unitOfWork.Certifications.Delete(certification);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
