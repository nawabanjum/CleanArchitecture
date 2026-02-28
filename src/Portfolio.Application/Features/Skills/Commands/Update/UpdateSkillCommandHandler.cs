using AutoMapper;
using MediatR;
using Portfolio.Application.Common.Exceptions;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Skills.Commands.Update;

public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand, SkillDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public UpdateSkillCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<SkillDto> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.Portfolios.GetByIdAsync(request.PortfolioId)
            ?? throw new NotFoundException(nameof(Portfolio), request.PortfolioId);

        if (portfolio.UserId != _currentUserService.UserId)
            throw new ForbiddenException();

        var skill = await _unitOfWork.Skills.GetByIdAsync(request.Id)
            ?? throw new NotFoundException("Skill", request.Id);

        _mapper.Map(request, skill);
        skill.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Skills.Update(skill);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<SkillDto>(skill);
    }
}
