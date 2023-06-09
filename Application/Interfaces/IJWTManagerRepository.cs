using Application.Models.Token;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IJWTManagerRepository
    {
        Tokens GenerateToken(User user);
        string GenerateRefreshToken();
    }
}
