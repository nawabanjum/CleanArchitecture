using Portfolio.Application.DTOs;
using Portfolio.Application.Features.Auth.Commands.Login;
using Portfolio.Application.Features.Auth.Commands.Register;

namespace Portfolio.Application.Common.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterCommand command);
    Task<AuthResponseDto> LoginAsync(LoginCommand command);
}
