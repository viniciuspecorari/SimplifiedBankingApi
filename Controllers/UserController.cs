using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimplifiedBankingApi.Contracts;
using SimplifiedBankingApi.Models.Dto;
using System.Security.Claims;

namespace SimplifiedBankingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IWalletRepository _walletRepository;

        public UserController(IUserRepository userRepository, IAuthRepository authRepository, IWalletRepository walletRepository)
        {
            _userRepository = userRepository;
            _authRepository = authRepository;
            _walletRepository = walletRepository;

        }
        
        [HttpPost]
        [Route("/api/register")]
        public async Task<IActionResult> Add(UserDto userDto)
        {
            await _userRepository.Add(userDto);

            var user = await _userRepository.GetByEmail(userDto.Email);
            if (user != null)
            {
                await _walletRepository.Add(user.Id);
            }            

            //var userid = _authRepository.GetUserToken(tokenString);


            return Ok();
        }
    }
}
