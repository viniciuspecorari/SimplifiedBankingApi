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

        public UserController(IUserRepository userRepository, IAuthRepository authRepository)
        {
            _userRepository = userRepository;
            _authRepository = authRepository;

        }

        //[Authorize]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        //{                               
        //    var users = await _userRepository.Get();
            
        //    return Ok(users);
        //}
        
        [HttpPost]
        public async Task<IActionResult> Add(UserDto userDto)
        {
            await _userRepository.Add(userDto);

            //var userid = _authRepository.GetUserToken(tokenString);


            return Ok();
        }
    }
}
