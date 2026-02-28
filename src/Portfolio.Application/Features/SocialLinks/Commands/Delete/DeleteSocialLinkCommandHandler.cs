using MediatR;
using Portfolio.Application.Common.Exceptions;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.SocialLinks.Commands.Delete;

public class DeleteSocialLinkCommandHandler : IRequestHandler<DeleteSocialLinkCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public DeleteSocialLinkCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(DeleteSocialLinkCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.Portfolios.GetByIdAsync(request.PortfolioId)
            ?? throw new NotFoundException(nameof(Portfolio), request.PortfolioId);

        if (portfolio.UserId != _currentUserService.UserId)
            throw new ForbiddenException();

        var socialLink = await _unitOfWork.SocialLinks.GetByIdAsync(request.Id)
            ?? throw new NotFoundException("SocialLink", request.Id);

        _unitOfWork.SocialLinks.Delete(socialLink);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
