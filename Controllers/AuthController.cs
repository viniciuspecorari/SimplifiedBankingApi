using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimplifiedBankingApi.Contracts;
using SimplifiedBankingApi.Models;
using SimplifiedBankingApi.Models.Dto;

namespace SimplifiedBankingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("login", Name = "login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Auth>> Login(LoginDto login)
        {
            var token = await _authRepository.GenerateToken(login);           

            if (token is null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

    }
}
