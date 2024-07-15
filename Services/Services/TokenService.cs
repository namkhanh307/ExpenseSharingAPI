using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.AuthModel;
using Repositories.ResponseModel.PersonModel;
using Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public TokenService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public GetTokenModel GenerateTokens(Person person)  
        {
            DateTime now = DateTime.Now;

            // Common claims for both tokens
            List<Claim> claims = new List<Claim>
            {
                new Claim("id", person.Id.ToString()),
                new Claim("exp", now.Ticks.ToString())
            };

            var keyString = _configuration.GetSection("JWT:SecretKey").Value ?? string.Empty;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));

            var claimsIdentity = new ClaimsIdentity(claims, "Bearer");
            var principal = new ClaimsPrincipal(new[] { claimsIdentity });
            _httpContextAccessor.HttpContext.User = principal;
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            // Generate access token
            var accessToken = new JwtSecurityToken(
                claims: claims,
                issuer: _configuration.GetSection("JWT:Issuer").Value,
                audience: _configuration.GetSection("JWT:Audience").Value,
                expires: now.AddMinutes(30),
                signingCredentials: creds
            );
            var accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);

            // Generate refresh token
            var refreshToken = new JwtSecurityToken(
                claims: claims,
                issuer: _configuration.GetSection("JWT:Issuer").Value,
                audience: _configuration.GetSection("JWT:Audience").Value,
                expires: now.AddDays(7),
                signingCredentials: creds
            );
            var refreshTokenString = new JwtSecurityTokenHandler().WriteToken(refreshToken);
            // Return the tokens and user information
            return new GetTokenModel
            {
                AccessToken = accessTokenString,
                RefreshToken = refreshTokenString            
            };
        }
    }
}
