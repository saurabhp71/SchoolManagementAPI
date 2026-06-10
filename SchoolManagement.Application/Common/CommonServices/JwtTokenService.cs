using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Application.Common.CommonInterface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace SchoolManagement.Application.Common.CommonServices
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(int employeeId, string userName, string roleName)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
                            {
                                new Claim("EmployeeId", employeeId.ToString()),
                                new Claim(ClaimTypes.Name, userName),
                                new Claim(ClaimTypes.Role, roleName)
                            };

            var token = new JwtSecurityToken(
                            issuer: _configuration["JwtSettings:Issuer"],
                            audience: _configuration["JwtSettings:Audience"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(
                                Convert.ToDouble(
                                    _configuration["JwtSettings:ExpiryMinutes"])),
                            signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];

            using var rng =
                RandomNumberGenerator.Create();

            rng.GetBytes(randomBytes);

            return Convert.ToBase64String(randomBytes);
        }
    }
}
