using AutoMapper;
using MediatR;
using Portfolio.Application.Common.Exceptions;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.SocialLinks.Commands.Update;

public class UpdateSocialLinkCommandHandler : IRequestHandler<UpdateSocialLinkCommand, SocialLinkDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public UpdateSocialLinkCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<SocialLinkDto> Handle(UpdateSocialLinkCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.Portfolios.GetByIdAsync(request.PortfolioId)
            ?? throw new NotFoundException(nameof(Portfolio), request.PortfolioId);

        if (portfolio.UserId != _currentUserService.UserId)
            throw new ForbiddenException();

        var socialLink = await _unitOfWork.SocialLinks.GetByIdAsync(request.Id)
            ?? throw new NotFoundException("SocialLink", request.Id);

        _mapper.Map(request, socialLink);
        socialLink.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.SocialLinks.Update(socialLink);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<SocialLinkDto>(socialLink);
    }
}
