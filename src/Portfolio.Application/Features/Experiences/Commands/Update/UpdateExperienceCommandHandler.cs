using AutoMapper;
using MediatR;
using Portfolio.Application.Common.Exceptions;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Experiences.Commands.Update;

public class UpdateExperienceCommandHandler : IRequestHandler<UpdateExperienceCommand, ExperienceDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public UpdateExperienceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<ExperienceDto> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.Portfolios.GetByIdAsync(request.PortfolioId)
            ?? throw new NotFoundException(nameof(Portfolio), request.PortfolioId);

        if (portfolio.UserId != _currentUserService.UserId)
            throw new ForbiddenException();

        var experience = await _unitOfWork.Experiences.GetByIdAsync(request.Id)
            ?? throw new NotFoundException("Experience", request.Id);

        _mapper.Map(request, experience);
        experience.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Experiences.Update(experience);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ExperienceDto>(experience);
    }
}
