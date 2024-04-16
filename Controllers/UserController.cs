using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimplifiedBankingApi.Contracts;
using SimplifiedBankingApi.Models.Dto;

namespace SimplifiedBankingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserDto userDto)
        {
            await _userRepository.Add(userDto);

            return Ok();
        }
    }
}
