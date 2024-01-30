using CleanMultitenant.Domain.Configurations;
using CleanMultitenant.Domain.Models.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CleanMultitenant.Infra.Services.Security
{
    public class SecurityService : ISecurityService
    {
        private readonly JwtConfig _jwtConfig;
        private readonly IHttpContextAccessor _contextAccessor;

        public SecurityService(IOptions<JwtConfig> jwtConfig, IHttpContextAccessor contextAccessor)
        {
            _jwtConfig = jwtConfig.Value;
            _contextAccessor = contextAccessor;
        }

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtConfig.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims: GenerateClaims(user),
                expires: DateTime.UtcNow.AddMinutes(_jwtConfig.ExpirationMinutes),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetTenant()
        {
            ArgumentNullException.ThrowIfNull(_contextAccessor?.HttpContext?.Request.RouteValues["slugTenant"]?.ToString());

            var tenant = _contextAccessor.HttpContext!.Request.RouteValues["slugTenant"]!.ToString();
            return tenant!;
        }

        private IEnumerable<Claim> GenerateClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Expiration, TimeSpan.FromMinutes(_jwtConfig.ExpirationMinutes).Ticks.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };
            return claims;
        }
    }
}
