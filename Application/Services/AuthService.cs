using Application.DTOs.Auth;
using Domain.Entities;
using Domain.Interfaces.Auth;

namespace Application.Services;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IPasswordService _passwordService;

    public AuthService(
        IUserRepository userRepository,
        IJwtService jwtService,
        IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _passwordService = passwordService;
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetAsync(m=>m.Email==request.Email);

        if (user == null || !user.IsActive || !_passwordService.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            return null;
        }

        var token = _jwtService.GenerateToken(user);

        return new AuthResponse(token, user.Email);
    }
}