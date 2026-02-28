using AutoMapper;
using MediatR;
using Portfolio.Application.Common.Exceptions;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Educations.Commands.Update;

public class UpdateEducationCommandHandler : IRequestHandler<UpdateEducationCommand, EducationDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public UpdateEducationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<EducationDto> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.Portfolios.GetByIdAsync(request.PortfolioId)
            ?? throw new NotFoundException(nameof(Portfolio), request.PortfolioId);

        if (portfolio.UserId != _currentUserService.UserId)
            throw new ForbiddenException();

        var education = await _unitOfWork.Educations.GetByIdAsync(request.Id)
            ?? throw new NotFoundException("Education", request.Id);

        _mapper.Map(request, education);
        education.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Educations.Update(education);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<EducationDto>(education);
    }
}
