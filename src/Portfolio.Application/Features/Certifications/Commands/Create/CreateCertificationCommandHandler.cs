using AutoMapper;
using MediatR;
using Portfolio.Application.Common.Exceptions;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Certifications.Commands.Create;

public class CreateCertificationCommandHandler : IRequestHandler<CreateCertificationCommand, CertificationDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public CreateCertificationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<CertificationDto> Handle(CreateCertificationCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.Portfolios.GetByIdAsync(request.PortfolioId)
            ?? throw new NotFoundException(nameof(Portfolio), request.PortfolioId);

        if (portfolio.UserId != _currentUserService.UserId)
            throw new ForbiddenException();

        var certification = _mapper.Map<Domain.Entities.Certification>(request);
        await _unitOfWork.Certifications.AddAsync(certification);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<CertificationDto>(certification);
    }
}
