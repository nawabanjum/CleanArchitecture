using AutoMapper;
using MediatR;
using Portfolio.Application.Common.Exceptions;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Experiences.Commands.Create;

public class CreateExperienceCommandHandler : IRequestHandler<CreateExperienceCommand, ExperienceDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public CreateExperienceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<ExperienceDto> Handle(CreateExperienceCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.Portfolios.GetByIdAsync(request.PortfolioId)
            ?? throw new NotFoundException(nameof(Portfolio), request.PortfolioId);

        if (portfolio.UserId != _currentUserService.UserId)
            throw new ForbiddenException();

        var experience = _mapper.Map<Domain.Entities.Experience>(request);
        await _unitOfWork.Experiences.AddAsync(experience);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ExperienceDto>(experience);
    }
}
