using AutoMapper;
using MediatR;
using Portfolio.Application.Common.Exceptions;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.SocialLinks.Commands.Create;

public class CreateSocialLinkCommandHandler : IRequestHandler<CreateSocialLinkCommand, SocialLinkDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public CreateSocialLinkCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<SocialLinkDto> Handle(CreateSocialLinkCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.Portfolios.GetByIdAsync(request.PortfolioId)
            ?? throw new NotFoundException(nameof(Portfolio), request.PortfolioId);

        if (portfolio.UserId != _currentUserService.UserId)
            throw new ForbiddenException();

        var socialLink = _mapper.Map<Domain.Entities.SocialLink>(request);
        await _unitOfWork.SocialLinks.AddAsync(socialLink);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<SocialLinkDto>(socialLink);
    }
}
