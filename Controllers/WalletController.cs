using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimplifiedBankingApi.Contracts;
using SimplifiedBankingApi.Models.Dto;
using SimplifiedBankingApi.Repository;

namespace SimplifiedBankingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IAuthRepository _authRepository;

        public WalletController(IWalletRepository walletRepository, IAuthRepository authRepository)
        {
            _walletRepository = walletRepository;
            _authRepository = authRepository;
        }

        [Authorize] 
        [HttpPut]
        [Route("/api/deposit")]
        public async Task<IActionResult> Deposit(WalletUpdatedDto walletUpdate)
        {
            var UserAuthWalletId = await _authRepository.GetUserIdToken(); // Recupera walletId do usuário logado
            await _walletRepository.Deposit(UserAuthWalletId.Id, walletUpdate.WalletBalance);

            return Ok();
        }
    }
}
