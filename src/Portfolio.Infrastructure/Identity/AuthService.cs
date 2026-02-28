using Microsoft.AspNetCore.Identity;
using Portfolio.Application.Common.Exceptions;
using Portfolio.Application.Common.Interfaces;
using Portfolio.Application.DTOs;
using Portfolio.Application.Features.Auth.Commands.Login;
using Portfolio.Application.Features.Auth.Commands.Register;
using Portfolio.Domain.Entities.Identity;

namespace Portfolio.Infrastructure.Identity;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        JwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterCommand command)
    {
        var existingUser = await _userManager.FindByEmailAsync(command.Email);
        if (existingUser != null)
            throw new BadRequestException("A user with this email already exists.");

        var user = new ApplicationUser
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            UserName = command.Email
        };

        var result = await _userManager.CreateAsync(user, command.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new BadRequestException($"Registration failed: {errors}");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginCommand command)
    {
        var user = await _userManager.FindByEmailAsync(command.Email)
            ?? throw new BadRequestException("Invalid email or password.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, command.Password, false);
        if (!result.Succeeded)
            throw new BadRequestException("Invalid email or password.");

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }
}
