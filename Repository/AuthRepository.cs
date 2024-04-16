using Azure.Core;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using SimplifiedBankingApi.Contracts;
using SimplifiedBankingApi.Exceptions;
using SimplifiedBankingApi.Models;
using SimplifiedBankingApi.Models.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimplifiedBankingApi.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthRepository(IConfiguration configuration, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<Auth> GenerateToken(LoginDto login)
        {
            var user = await _userRepository.GetByEmail(login.Email);

            if (login.Email != user.Email || login.Password != user.Password)
            {
                throw new Error(StatusCodes.Status404NotFound.ToString(), "User invalid", login.Email.ToString());
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var singinCredential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: new[]
                {
                    new Claim(type: "Id", user.Id.ToString()),
                    new Claim(type: "Email", user.Email),
                    new Claim(type: "Type", user.Type.ToString())
                },
                expires: DateTime.Now.AddHours(2),
                signingCredentials: singinCredential
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new Auth
            {
                Token = token,
                Expired = DateTime.Now.AddHours(2)
            };
        }

        public async Task<string> GetUserIdToken()
        {
            var tokenString = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Split(" ").Last();

            // Decodificar o token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(tokenString);

            // Obter informações do usuário a partir das claims
            var userId = token.Claims.First(claim => claim.Type == "Id").Value;
            

            // Agora você pode usar as informações do usuário como necessário
            return userId;
        }

    }
}
