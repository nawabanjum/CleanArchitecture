using AutoMapper;
using MediatR;
using Portfolio.Application.Common.Exceptions;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Certifications.Commands.Update;

public class UpdateCertificationCommandHandler : IRequestHandler<UpdateCertificationCommand, CertificationDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public UpdateCertificationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<CertificationDto> Handle(UpdateCertificationCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.Portfolios.GetByIdAsync(request.PortfolioId)
            ?? throw new NotFoundException(nameof(Portfolio), request.PortfolioId);

        if (portfolio.UserId != _currentUserService.UserId)
            throw new ForbiddenException();

        var certification = await _unitOfWork.Certifications.GetByIdAsync(request.Id)
            ?? throw new NotFoundException("Certification", request.Id);

        _mapper.Map(request, certification);
        certification.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Certifications.Update(certification);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<CertificationDto>(certification);
    }
}
