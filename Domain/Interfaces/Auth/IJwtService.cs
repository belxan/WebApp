using System.Security.Claims;

namespace Domain.Interfaces.Auth;

public interface IJwtService
{
    string GenerateToken(User user);
    ClaimsPrincipal? ValidateToken(string token);
}
