using Application.Interfaces;
using Application.Models.Token;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly Abstraction.IApplicationDbContext _context;
        private readonly IConfiguration configuration;

        public JWTManagerRepository(IConfiguration iconfiguration, Abstraction.IApplicationDbContext context)
        {
            configuration = iconfiguration;
            _context = context;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public Tokens GenerateToken(User user)
        {

            var role = new List<Claim>();
            foreach (var item in user.Roles)
            {
                role.Add(new Claim(ClaimTypes.Role, item.ToString()));
            }
            Claim[] Claims = new[]
            {
                new Claim("Email", user.Email),
                new Claim("Password", user.Password)
            };

            role.AddRange(Claims);

            JwtSecurityToken token = new(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: role,
                expires: DateTime.UtcNow.AddMinutes(50),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])), SecurityAlgorithms.HmacSha256)
                );
            var _token = new JwtSecurityTokenHandler().WriteToken(token);

            Tokens tokens = new Tokens
            {
                Access_Token = _token.ToString(),
                Refresh_Token = GenerateRefreshToken()
            };
            return tokens;
        }

    }
}
